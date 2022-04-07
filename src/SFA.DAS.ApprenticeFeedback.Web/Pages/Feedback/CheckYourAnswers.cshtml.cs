using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class CheckYourAnswersModel : PageModel, IHasBackLink
    {
        private IApprenticeFeedbackSessionService _sessionService;
        private IApprenticeFeedbackService _apprenticeFeedbackService;

        public CheckYourAnswersModel(IApprenticeFeedbackSessionService sessionService, IApprenticeFeedbackService apprenticeFeedbackService)
        {
            _sessionService = sessionService;
            _apprenticeFeedbackService = apprenticeFeedbackService;
        }

        public string TrainingProvider { get; set; }
        public List<FeedbackAttribute> FeedbackAttributes { get; set; }
        public OverallRating OverallRating { get; set; }

        [BindProperty]
        public bool ContactConsent { get; set; }

        public string Backlink => "/overall-rating";

        public void OnGet()
        {
            var feedbackRequest = _sessionService.GetFeedbackRequest();

            TrainingProvider = feedbackRequest.TrainingProvider;
            FeedbackAttributes = feedbackRequest.FeedbackAttributes;
            OverallRating = feedbackRequest.OverallRating.Value;
        }

        public async Task<IActionResult> OnPost([FromServices] AuthenticatedUser user)
        {            
            var feedbackRequest = _sessionService.GetFeedbackRequest();
            var submitFeedbackRequest = new PostSubmitFeedback()
            {
                ApprenticeId = user.ApprenticeId,
                ProviderName = feedbackRequest.TrainingProvider,
                LarsCode = feedbackRequest.LarsCode,
                StandardReference = feedbackRequest.StandardReference,
                StandardUId = feedbackRequest.StandardUId,
                Ukprn = feedbackRequest.Ukprn,
                OverallRating = feedbackRequest.OverallRating.Value,
                FeedbackAttributes = feedbackRequest.FeedbackAttributes
            };

            await _apprenticeFeedbackService.SubmitFeedback(submitFeedbackRequest);

            return RedirectToPage("Complete");
        }
    }
}
