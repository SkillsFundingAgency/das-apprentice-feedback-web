using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    public class CompleteModel : ExitSurveyContextPageModel
    {
        public CompleteModel(IExitSurveySessionService sessionService
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
