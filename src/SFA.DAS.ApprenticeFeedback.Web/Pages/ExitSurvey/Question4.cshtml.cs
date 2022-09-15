using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class Question4Model : ExitSurveyContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Please select an answer")]
        public string RemainedReason { get; set; }
        public string[] Reasons = new[]
        {
            "I had more support and guidance from my employer",
            "I had more support and guidance from my training provider",
            "I had more information on how it would be assessed at end point assessment",
            "My employer did not end my apprenticeship",
            "My training provider did not end my apprenticeship",
            "The employer gave me more time to undertake learning and training",
            "The employer provided me with the training they were meant to",
            "The training provider delivered better quality training",
            "There was a mentor or learning coach within or outside of my organisation",
        };

        public string Backlink => (ExitSurveyContext.CheckingAnswers) ? $"./checkyouranswers" : $"./question3";

        public Question4Model(IExitSurveySessionService sessionService)
            : base(sessionService)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            RemainedReason = ExitSurveyContext.RemainedReason;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitSurveyContext.RemainedReason = RemainedReason;
            SaveContext();

            return RedirectToPage("./checkyouranswers");
        }
    }
}