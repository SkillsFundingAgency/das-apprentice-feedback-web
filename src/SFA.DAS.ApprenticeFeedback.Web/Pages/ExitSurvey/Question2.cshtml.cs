using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class Question2Model : ExitSurveyContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Select the reason why you did not complete the apprenticeship")]
        public string IncompletionReason { get; set; }
        public ReadOnlyCollection<string> Reasons { get { return _reasons.AsReadOnly(); } }

        public string Backlink => (ExitSurveyContext.CheckingAnswers)? $"./checkyouranswers" : $"./question1";

        private readonly List<string> _reasons = new List<string>
        {
            "I did not enjoy it",
            "I had personal or health reasons",
            "I experienced discrimination or poor behaviour",
            "I started another apprenticeship",
            "I started another job",
            "I was made redundant",
            "The job was too difficult",
            "The job was not what I expected",
            "The salary did not meet my financial needs",
            "The training was repetitive",
            "The training was too difficult",
            "The training was of poor quality",
            "The training was not relevant to the job role",
            "The training provider stopped delivering apprenticeships",
            "The training provider ended my apprenticeship",
            "There were issues with my end-point assessment",
            "None of the above"
        };

        public Question2Model(IExitSurveySessionService sessionService)
            : base(sessionService, Domain.Models.ExitSurvey.UserJourney.DidNotComplete)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            IncompletionReason = ExitSurveyContext.IncompletionReason;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitSurveyContext.IncompletionReason = IncompletionReason;
            SaveContext();

            if (ExitSurveyContext.CheckingAnswers)
            {
                return RedirectToPage("./checkyouranswers");
            }
            else
            {
                return RedirectToPage("./question3");
            }
        }
    }
}