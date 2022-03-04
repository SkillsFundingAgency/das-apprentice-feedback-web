using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class StartModel : PageModel
    {
        private IApprenticeFeedbackSessionService _sessionService;

        public StartModel(IApprenticeFeedbackSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public string ProviderName { get; set; }

        public void OnGet([FromServices] AuthenticatedUser user)
        {
            var request = _sessionService.GetFeedbackRequest();

            ProviderName = request.TrainingProvider;
        }

        public IActionResult OnPost()
        {
           // _sessionService.StartNewFeedbackRequest();

            return RedirectToPage("FeedbackAttributes");
        }
    }
}
