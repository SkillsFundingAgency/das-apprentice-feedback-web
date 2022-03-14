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
        public string FATLink { get; set; }
        //from data in the request build FAT link
        //will have to store environment variable in FAT config and use in base url and add string interpolation to add rest of link
        //inject the config into the helper class (in application, service layer) and inject helper class into the start page
        ////going to need an interface - iurlhelper
        //

        public void OnGet()//[FromServices] AuthenticatedUser user)
        {
            var request = _sessionService.GetFeedbackRequest();

            ProviderName = request.TrainingProvider;
        }

        public IActionResult OnPost()
        {
            // SV-275 - change to link to question page form post no longer required as session starts on previous page
            return RedirectToPage("FeedbackAttributes");
        }
    }
}
