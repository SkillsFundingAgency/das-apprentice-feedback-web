using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitInterview
{
    public class CompleteModel : ExitInterviewContextPageModel
    {
        public CompleteModel(IExitInterviewSessionService sessionService)
            : base(sessionService)
        {
        }

        public void OnGet()
        {
        }
    }
}
