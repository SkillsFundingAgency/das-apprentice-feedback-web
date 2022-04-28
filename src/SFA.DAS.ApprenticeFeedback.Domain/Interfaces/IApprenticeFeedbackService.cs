using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IApprenticeFeedbackService
    {
        //TimeSpan InitialDenyPeriod { get; }
        //TimeSpan RecentDenyPeriod { get; }
        //TimeSpan FinalAllowPeriod { get; }

        Task<IEnumerable<TrainingProvider>> GetTrainingProviders(Guid apprenticeId);
        Task<TrainingProvider> GetTrainingProvider(Guid apprenticeId, long ukprn);
        Task<IEnumerable<FeedbackAttribute>> GetFeedbackAttributes();
        Task SubmitFeedback(PostSubmitFeedback request);
        //(FeedbackEligibility feedbackEligibility, DateTime? significantDate, TimeSpan? timeWindow) GetFeedbackEligibility(Apprenticeship apprenticeship);
    }
}
