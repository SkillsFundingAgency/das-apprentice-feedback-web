using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public interface IApprenticeFeedbackSessionService
    {
        void StartNewFeedbackRequest();
        void StartNewFeedbackRequest(string provderName, long ukprn, int larsCode);
        FeedbackRequest GetFeedbackRequest();
        void UpdateFeedbackRequest(FeedbackRequest request);
    }
}
