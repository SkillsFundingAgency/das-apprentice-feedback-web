using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitInterview;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public class ExitInterviewSessionService : IExitInterviewSessionService
    {
        private readonly ISessionService _sessionService;

        private const string _sessionKey = "Exit_Interview_Context";

        public ExitInterviewSessionService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public ExitInterviewContext GetExitInterviewContext()
        {
            return _sessionService.Get<ExitInterviewContext>(_sessionKey);
        }

        public void SetExitInterviewContext(ExitInterviewContext exitInterviewContext)
        {
            _sessionService.Set(_sessionKey, exitInterviewContext);
        }
    }
}
