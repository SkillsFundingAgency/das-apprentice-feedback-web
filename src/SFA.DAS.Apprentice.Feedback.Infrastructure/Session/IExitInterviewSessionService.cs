using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitInterview;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public interface IExitInterviewSessionService
    {
        ExitInterviewContext GetExitInterviewContext();
        void SetExitInterviewContext(ExitInterviewContext exitInterviewContext);
    }
}
