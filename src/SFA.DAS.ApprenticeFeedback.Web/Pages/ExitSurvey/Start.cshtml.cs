using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class StartModel : ExitSurveyContextPageModel
    {
        public StartModel(IExitSurveySessionService sessionService)
            :base(sessionService)
        {
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user, Guid apprenticeFeedbackTargetId)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            // Remember the apprentice feedback target id
            ExitSurveyContext.ApprenticeFeedbackTargetId = apprenticeFeedbackTargetId;
            SaveContext();

            return Page();
        }
    }
}