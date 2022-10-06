using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    public class CompleteModel : ExitSurveyContextPageModel
    {
        public CompleteModel(IExitSurveySessionService sessionService)
            : base(sessionService, Domain.Models.ExitSurvey.UserJourney.Finished)
        {
        }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
