using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public interface IApprenticeFeedbackSessionService
    {
        void SetFeedbackContext(FeedbackContext context);
        FeedbackContext GetFeedbackContext();
    }
}
