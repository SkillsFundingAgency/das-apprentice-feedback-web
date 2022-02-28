using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;

namespace SFA.DAS.Apprentice.Feedback.Infrastructure.SessionService
{
    public interface IApprenticeFeedbackSessionService
    {
        void StartNewFeedbackRequest();
        FeedbackRequest GetFeedbackRequest();
        void UpdateFeedbackRequest(FeedbackRequest request);
    }
}
