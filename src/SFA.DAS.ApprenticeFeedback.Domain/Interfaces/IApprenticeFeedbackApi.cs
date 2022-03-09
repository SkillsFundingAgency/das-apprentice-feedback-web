﻿using RestEase;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IApprenticeFeedbackApi
    {
        [Post("/")]
        Task SubmitFeedback(PostSubmitFeedback feedback);

        [Get("/apprentices/{id}")]
        Task<ApprenticeResponse> GetApprentice([Path] Guid id);

        [Get("/feedback/{apprenticeId}")]
        Task<GetTrainingProvidersResponse> GetTrainingProviders([Path] Guid apprenticeId);

        [Get("/feedback/{apprenticeId}/{ukprn}")]
        Task<TrainingProvider> GetTrainingProvider([Path] Guid apprenticeId, [Path] long ukprn);

        [Get("/provider-attributes")]
        Task<GetProviderAttributesResponse> GetProviderAttributes();

    }
}
