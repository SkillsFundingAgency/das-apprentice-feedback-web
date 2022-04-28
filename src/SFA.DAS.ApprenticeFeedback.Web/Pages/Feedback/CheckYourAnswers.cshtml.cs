using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class CheckYourAnswersModel : FeedbackContextPageModel, IHasBackLink
    {
        public List<FeedbackAttribute> FeedbackAttributes { get; set; }
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
            FeedbackAttributes = FeedbackContext.FeedbackAttributes;
            OverallRating = FeedbackContext.OverallRating;

            return Page();
        }

        public async Task<IActionResult> OnPost([FromServices] AuthenticatedUser user)
        {
            var request = new PostSubmitFeedback
            {
                FeedbackAttributes = FeedbackContext.FeedbackAttributes,
                Ukprn = FeedbackContext.UkPrn,
                LarsCode = FeedbackContext.LarsCode,
                OverallRating = FeedbackContext.OverallRating.Value,
                ProviderName = FeedbackContext.ProviderName,
                ContactConsent = ContactConsent,
                ApprenticeId = user.ApprenticeId
            };
            await _apprenticeFeedbackService.SubmitFeedback(request);        

            return RedirectToPage("Complete");
        }
    }
}
