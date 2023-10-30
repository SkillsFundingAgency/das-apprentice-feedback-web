using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingProvider = SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback.TrainingProvider;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IApprenticeFeedbackService
    {
        TimeSpan InitialDenyPeriod { get; }
        TimeSpan RecentDenyPeriod { get; }
        TimeSpan FinalAllowPeriod { get; }

        Task<IEnumerable<TrainingProvider>> GetTrainingProviders(Guid apprenticeId);
        Task<TrainingProvider> GetTrainingProvider(Guid apprenticeId, long ukprn);
        Task<IEnumerable<FeedbackSurveyAttribute>> GetFeedbackAttributes();
        Task SubmitFeedback(PostSubmitFeedback request);
        Task SubmitExitSurvey(PostSubmitExitSurvey request);
        Task<IEnumerable<ApprenticeFeedbackTarget>> GetApprenticeFeedbackTargets(Guid apprenticeId);
        Task<ApprenticeExitSurvey> GetExitSurveyForFeedbackTarget(Guid feedbackTargetId);
        Task<IEnumerable<ExitSurveyAttribute>> GetExitSurveyAttributes(string category);
    }
}
