using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public class ApprenticeFeedbackSessionService : IApprenticeFeedbackSessionService
    {
        private readonly ISessionService _sessionService;

        private const string _sessionKey = "Apprentice_Feedback_Context";

        public ApprenticeFeedbackSessionService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public void SetFeedbackContext(FeedbackContext context)
        {
            _sessionService.Set(_sessionKey, context);
        }
        public FeedbackContext GetFeedbackContext()
        {
            return _sessionService.Get<FeedbackContext>(_sessionKey);
        }
    }
}
