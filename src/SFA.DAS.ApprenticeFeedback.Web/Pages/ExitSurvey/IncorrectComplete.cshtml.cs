using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class IncorrectCompleteModel : ExitSurveyContextPageModel
    {
        public IncorrectCompleteModel(IExitSurveySessionService sessionService
            , IApprenticeFeedbackService apprenticeFeedbackService)
           : base(sessionService, apprenticeFeedbackService)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
