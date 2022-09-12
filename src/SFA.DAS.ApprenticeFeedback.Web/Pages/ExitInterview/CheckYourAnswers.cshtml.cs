using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitInterview
{
    [HideNavigationBar]
    public class CheckYourAnswersModel : ExitInterviewContextPageModel, IHasBackLink
    {
        [BindProperty]
        public bool ContactMe { get; set; }

        public string Backlink => $"./question4";

        public CheckYourAnswersModel(IExitInterviewSessionService sessionService)
            : base(sessionService)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            ExitInterviewContext.CheckingAnswers = true;
            SaveContext();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitInterviewContext.ContactMe = ContactMe;
            SaveContext();

            // Save the context values to the database

            return RedirectToPage("./complete");
        }
    }
}