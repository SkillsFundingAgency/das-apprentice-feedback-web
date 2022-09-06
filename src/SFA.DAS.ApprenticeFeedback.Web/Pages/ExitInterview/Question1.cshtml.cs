using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitInterview
{
    [HideNavigationBar]
    public class Question1Model : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Select if you completed your apprenticeship")]
        public bool? CompleteApprenticeship { get; set; }

        public Question1Model()
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //FeedbackContext.OverallRating = OverallRating;
            //SaveFeedbackContext();

            return RedirectToPage("exit/question2");
        }
    }
}