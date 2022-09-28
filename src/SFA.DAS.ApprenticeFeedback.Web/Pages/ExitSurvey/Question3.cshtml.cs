using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class Question3Model : ExitSurveyContextPageModel, IHasBackLink
    {
        [BindProperty]
        public bool IncompletionFactor_Caring { get; set; }
        [BindProperty]
        public bool IncompletionFactor_Family { get; set; }
        [BindProperty]
        public bool IncompletionFactor_Financial { get; set; }
        [BindProperty]
        public bool IncompletionFactor_Mental { get; set; }
        [BindProperty]
        public bool IncompletionFactor_Physical { get; set; }
        [BindProperty]
        public bool IncompletionFactor_None { get; set; }

        public string Backlink => (ExitSurveyContext.CheckingAnswers) ? $"./checkyouranswers" : $"./question2";

        public ReadOnlyCollection<string> Factors { get { return _factors.AsReadOnly(); } }
        private readonly List<string> _factors = new List<string>
        {
            "Caring responsibilities",
            "Family or relationship issues",
            "Financial issues",
            "Mental health",
            "Physical health issues",
            "None of the above"
        };

        public Question3Model(IExitSurveySessionService sessionService)
            : base(sessionService)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            IncompletionFactor_Caring = ExitSurveyContext.IncompletionFactor_Caring;
            IncompletionFactor_Family = ExitSurveyContext.IncompletionFactor_Family;
            IncompletionFactor_Financial = ExitSurveyContext.IncompletionFactor_Financial;
            IncompletionFactor_Mental = ExitSurveyContext.IncompletionFactor_Mental;
            IncompletionFactor_Physical = ExitSurveyContext.IncompletionFactor_Physical;
            IncompletionFactor_None = ExitSurveyContext.IncompletionFactor_None;

            return Page();
        }

        public IActionResult OnPost()
        {
            bool factorSelected = IncompletionFactor_Caring
                || IncompletionFactor_Family
                || IncompletionFactor_Financial
                || IncompletionFactor_Mental
                || IncompletionFactor_Physical;
            bool otherSelected = IncompletionFactor_None;
            if((!factorSelected && !otherSelected)
                || factorSelected && otherSelected)
            {
                ModelState.AddModelError("MultipleErrorSummary", "Select the factors relevant to you, or select 'None of the above'");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitSurveyContext.IncompletionFactor_Caring = IncompletionFactor_Caring;
            ExitSurveyContext.IncompletionFactor_Family = IncompletionFactor_Family;
            ExitSurveyContext.IncompletionFactor_Financial = IncompletionFactor_Financial;
            ExitSurveyContext.IncompletionFactor_Mental = IncompletionFactor_Mental;
            ExitSurveyContext.IncompletionFactor_Physical = IncompletionFactor_Physical;
            ExitSurveyContext.IncompletionFactor_None = IncompletionFactor_None;
            SaveContext();

            if (ExitSurveyContext.CheckingAnswers)
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