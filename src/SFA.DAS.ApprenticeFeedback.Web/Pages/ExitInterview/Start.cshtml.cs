using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitInterview
{
    [HideNavigationBar]
    public class StartModel : PageModel
    {
        public StartModel()
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.
            return Page();
        }
    }
}