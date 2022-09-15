using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public class ExitSurveySessionService : IExitSurveySessionService
    {
        private readonly ISessionService _sessionService;

        private const string _sessionKey = "Exit_Interview_Context";

        public ExitSurveySessionService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public ExitSurveyContext GetExitSurveyContext()
        {
            return _sessionService.Get<ExitSurveyContext>(_sessionKey);
        }

        public void SetExitSurveyContext(ExitSurveyContext ExitSurveyContext)
        {
            _sessionService.Set(_sessionKey, ExitSurveyContext);
        }
    }
}
