using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class CompleteModel : PageModel
    {
        private IApprenticeFeedbackSessionService _sessionService;

        public CompleteModel(IApprenticeFeedbackSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public string TrainingProvider { get; set; }
        public OverallRating OverallRating { get; set; }
        public bool IsPoorlyRated => OverallRating == OverallRating.Poor || OverallRating == OverallRating.VeryPoor;

        public void OnGet()
        {
            var feedbackRequest = _sessionService.GetFeedbackRequest();

            TrainingProvider = feedbackRequest.TrainingProvider;
            OverallRating = feedbackRequest.OverallRating.Value;
        }
    }
}
