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
        [Required(ErrorMessage = "Select a reason that would have helped you to stay on the apprenticeship")]
        public string RemainedReason { get; set; }
        public IEnumerable<string> Reasons { get { return _reasons; } }
        private readonly string[] _reasons = new[]
        {
            "A higher salary",
            "A mentor or learning coach",
            "Being able to skip training I have already done",
            "Better training from my employer",
            "Better training from my training provider",
            "More support from my employer",
            "More support from my training provider",
            "More information on the end-point assessment process",
            "More time to undertake learning and training with my training provider",
            "Outside support to tackle discrimination or other problems",
            "Reasonable adjustments from my training provider"
        };

        public string Backlink => (ExitSurveyContext.CheckingAnswers) ? $"./checkyouranswers" : $"./question3";

        public Question4Model(IExitSurveySessionService sessionService)
            : base(sessionService, Domain.Models.ExitSurvey.UserJourney.DidNotComplete)
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