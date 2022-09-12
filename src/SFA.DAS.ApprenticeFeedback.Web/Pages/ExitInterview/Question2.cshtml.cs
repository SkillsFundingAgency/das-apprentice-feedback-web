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
    public class Question2Model : ExitInterviewContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Please select an answer")]
        public string IncompletionReason { get; set; }
        public string[] Reasons = new[]
        {
            "I have already done something similar",
            "I was offered another role within the organisation",
            "I was offered another job outside of my organisation",
            "I was released from employment or made redundant",
            "It was not relevant to the job role",
            "There was too much coursework",
            "The training was difficult",
            "The job was difficult",
            "The quality of the training was poor",
            "The training provider did not think I would pass the end point assessment",
            "There was a delay in completing my end point assessment",
            "There was no career progression",
            "The salary did not meet my financial needs",
        };   

        public string Backlink => (ExitInterviewContext.CheckingAnswers)? $"./checkyouranswers" : $"./question1";

        public Question2Model(IExitInterviewSessionService sessionService)
            : base(sessionService)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            IncompletionReason = ExitInterviewContext.IncompletionReason;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitInterviewContext.IncompletionReason = IncompletionReason;
            SaveContext();

            if (ExitInterviewContext.CheckingAnswers)
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