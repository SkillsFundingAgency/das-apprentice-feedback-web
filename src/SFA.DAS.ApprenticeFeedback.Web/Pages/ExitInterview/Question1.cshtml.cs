using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitInterview
{
    [HideNavigationBar]
    public class Question1Model : ExitInterviewContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Select if you completed your apprenticeship")]
        public bool? DidNotCompleteApprenticeship { get; set; }

        public string Backlink => (ExitInterviewContext.CheckingAnswers) ? $"./checkyouranswers" : $"./start";


        public Question1Model(IExitInterviewSessionService sessionService)
            :base(sessionService)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            DidNotCompleteApprenticeship = ExitInterviewContext.DidNotCompleteApprenticeship;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // If the user changed the existing answer then forget the rest of the answers.
            if(ExitInterviewContext.DidNotCompleteApprenticeship.HasValue && ExitInterviewContext.DidNotCompleteApprenticeship != DidNotCompleteApprenticeship)
            {
                ExitInterviewContext.Reset();
            }

            ExitInterviewContext.DidNotCompleteApprenticeship = DidNotCompleteApprenticeship;
            SaveContext();

            if (ExitInterviewContext.DidNotCompleteApprenticeship.Equals(true))
            {
                if(ExitInterviewContext.CheckingAnswers)
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