using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
   // [RequiresIdentityConfirmed]
    public class FeedbackModel : PageModel
    {
        private readonly NavigationUrlHelper _urlHelper;

        public FeedbackModel(NavigationUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public void OnGet()//[FromServices] AuthenticatedUser user)
        {
        }
    }
}
