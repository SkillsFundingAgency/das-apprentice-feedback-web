using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IApprenticeFeedbackService
    {
        Task SubmitFeedback(PostSubmitFeedback request);

        Task<List<TrainingProvider>> GetTrainingProviders(Guid apprenticeId);
        Task<TrainingProvider> GetTrainingProvider(Guid apprenticeId, long ukprn);
        Task<List<FeedbackAttribute>> GetTrainingProviderAttributes();
    }
}
