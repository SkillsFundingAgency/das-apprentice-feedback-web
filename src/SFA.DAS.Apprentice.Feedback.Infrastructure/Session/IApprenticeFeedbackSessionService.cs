using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public interface IApprenticeFeedbackSessionService
    {
        void StartNewFeedbackRequest();
        FeedbackRequest GetFeedbackRequest();
        void UpdateFeedbackRequest(FeedbackRequest request);
    }
}
