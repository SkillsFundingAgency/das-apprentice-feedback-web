using RestEase;
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
    }
}
