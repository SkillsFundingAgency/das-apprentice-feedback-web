using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class IncorrectCompleteModel : ExitSurveyContextPageModel
    {
        public IncorrectCompleteModel(IExitSurveySessionService sessionService)
           : base(sessionService, Domain.Models.ExitSurvey.UserJourney.Finished)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
