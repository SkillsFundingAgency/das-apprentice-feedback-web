using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
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

        public async Task<IActionResult> OnPost()
        {
            var submitFeedbackRequest = new PostSubmitFeedback()
            {

            };

            await _apprenticeFeedbackService.SubmitFeedback(submitFeedbackRequest);

            return RedirectToPage("Complete");
        }
    }
}
