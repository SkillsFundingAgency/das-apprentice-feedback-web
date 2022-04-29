using RestEase;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IApprenticeFeedbackApi
    {
        [Get("/provider/{apprenticeId}")]
        Task<GetTrainingProvidersResponse> GetTrainingProviders([Path] Guid apprenticeId);

        [Get("provider-attributes")]
        Task<List<ProviderAttribute>> GetFeedbackAttributes();

        [Post("apprenticefeedback")]
        Task SubmitFeedback([Body] PostSubmitFeedback feedback);
    }
}
