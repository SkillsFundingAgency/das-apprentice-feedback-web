using RestEase;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IApprenticeFeedbackApi
    {
        [Get("/provider/{apprenticeId}")]
        Task<GetTrainingProvidersResponse> GetTrainingProviders([Path] Guid apprenticeId);

        [Get("provider/{apprenticeId}/{ukprn}")]
        Task<GetTrainingProviderResponse> GetTrainingProvider([Path] Guid apprenticeId, [Path] long ukprn);

        [Get("provider-attributes")]
        Task<List<FeedbackAttribute>> GetFeedbackAttributes();

        [Get("exitsurvey-attributes")]
        Task<List<FeedbackAttribute>> GetExitSurveyAttributes();

        [Post("apprenticefeedback")]
        Task SubmitFeedback([Body] PostSubmitFeedback feedback);

        [Get("/apprentice/{id}")]
        Task<Apprentice> GetApprentice([Path] Guid id);

        [Post("/exitsurvey")]
        Task SubmitExitSurvey([Body] PostSubmitExitSurvey exitSurvey);

        [Get("apprenticefeedbacktarget/{apprenticeId}")]
        Task<IEnumerable<ApprenticeFeedbackTarget>> GetApprenticeFeedbackTargets([Path] Guid apprenticeId);

        [Get("apprenticefeedbacktarget/{apprenticeFeedbackTargetId}/exitsurvey")]
        Task<ApprenticeExitSurvey> GetExitSurveyForFeedbackTarget([Path] Guid apprenticeFeedbackTargetId);
    }
}
