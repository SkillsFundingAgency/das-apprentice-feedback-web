using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    public class CheckYourAnswersModel : FeedbackContextPageModel, IHasBackLink
    {
        public List<FeedbackSurveyAttribute> FeedbackAttributes { get; set; }
        public OverallRating? OverallRating { get; set; }

        [BindProperty]
        public bool ContactConsent { get; set; }

        public string Backlink => "/overall-rating";

        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;

        public CheckYourAnswersModel(IApprenticeFeedbackSessionService sessionService, IApprenticeFeedbackService apprenticeFeedbackService)
            :base(sessionService)
        {
            _apprenticeFeedbackService = apprenticeFeedbackService;
        }

        public IActionResult OnGet()
        {
            // Prevent a user from resubmitting feedback.
            if(FeedbackContext.FeedbackEligibility != FeedbackEligibility.Allow)
            {
                return Redirect("/index");
            }

            FeedbackAttributes = FeedbackContext.FeedbackAttributes;
            OverallRating = FeedbackContext.OverallRating;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var request = new PostSubmitFeedback
            {
                FeedbackAttributes = FeedbackContext.FeedbackAttributes,
                OverallRating = FeedbackContext.OverallRating.Value,
                AllowContact = ContactConsent,
                ApprenticeFeedbackTargetId = FeedbackContext.ApprenticeFeedbackTargetId
            };
            await _apprenticeFeedbackService.SubmitFeedback(request);

            // Set the feedback status in session, to prevent the user from resubmitting feedback 
            FeedbackContext.FeedbackEligibility = FeedbackEligibility.Deny_HasGivenFeedbackRecently;
            SaveFeedbackContext();

            return RedirectToPage("Complete");
        }
    }
}
