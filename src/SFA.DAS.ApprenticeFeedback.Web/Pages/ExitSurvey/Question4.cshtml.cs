using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class Question4Model : ExitSurveyContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Select a reason why you would have remained on the apprenticeship")]
        public string RemainedReason { get; set; }
        public IEnumerable<string> Reasons { get { return _reasons; } }
        private readonly string[] _reasons = new[]
        {
            "I had more support and guidance from my employer",
            "I had more support and guidance from my training provider",
            "I had more information on the end-point assessment process",
            "I was not repeating training I had already done",
            "I was offered a higher salary",
            "my employer did not end my apprenticeship",
            "my training provider did not end my apprenticeship",
            "the apprenticeship was what I signed up for initially",
            "the employer provided me with the training they were meant to",
            "the training provider gave me more time to undertake learning and training",
            "the training provider had put in place the reasonable adjustments I needed to complete my training",
            "there was a mentor or learning coach within or outside of my organisation"
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