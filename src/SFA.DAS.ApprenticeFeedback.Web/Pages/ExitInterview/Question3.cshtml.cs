using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitInterview
{
    [HideNavigationBar]
    public class Question3Model : ExitInterviewContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Please select an answer")]
        public string IncompletionFactor { get; set; }
        public string[] Factors = new[]
        {
            "Caring responsibilities",
            "Family or relationship issues",
            "Mental health",
            "Physical health issues",
            "None of the above"
        };

        public string Backlink => (ExitInterviewContext.CheckingAnswers) ? $"./checkyouranswers" : $"./question2";

        public Question3Model(IExitInterviewSessionService sessionService)
            : base(sessionService)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            IncompletionFactor = ExitInterviewContext.IncompletionFactor;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitInterviewContext.IncompletionFactor = IncompletionFactor;
            SaveContext();

            if (ExitInterviewContext.CheckingAnswers)
            {
                return RedirectToPage("./checkyouranswers");
            }
            else
            {
                return RedirectToPage("./question4");
            }
        }
    }
}