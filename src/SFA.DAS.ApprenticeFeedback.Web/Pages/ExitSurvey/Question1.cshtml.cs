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
    public class Question1Model : ExitSurveyContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Confirm if our records are correct")]
        public bool? DidNotCompleteApprenticeship { get; set; }

        public string Backlink => (ExitSurveyContext.CheckingAnswers) ? $"./checkyouranswers" : $"./start/{ExitSurveyContext.ApprenticeFeedbackTargetId}";


        public Question1Model(IExitSurveySessionService sessionService)
            :base(sessionService)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            DidNotCompleteApprenticeship = ExitSurveyContext.DidNotCompleteApprenticeship;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // If the user changed the existing answer then forget the rest of the answers.
            if(ExitSurveyContext.DidNotCompleteApprenticeship.HasValue && ExitSurveyContext.DidNotCompleteApprenticeship != DidNotCompleteApprenticeship)
            {
                ExitSurveyContext.Reset();
            }

            ExitSurveyContext.DidNotCompleteApprenticeship = DidNotCompleteApprenticeship;
            SaveContext();

            if (ExitSurveyContext.DidNotCompleteApprenticeship.Equals(true))
            {
                if(ExitSurveyContext.CheckingAnswers)
                {
                    return RedirectToPage("./checkyouranswers");
                }
                else
                {
                    return RedirectToPage("./question2");
                }
            }
            return RedirectToPage("./incorrectreason");
        }
    }
}