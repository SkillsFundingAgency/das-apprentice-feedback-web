using RestEase;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Services
{
    /*
    public class ApprenticeFeedbackApiClient
    {
        private readonly IApprenticeFeedbackApi _client;

        public ApprenticeFeedbackApiClient(IApprenticeFeedbackApi client)
        {
            _client = client;
        }

        public async Task<ApprenticeResponse?> TryGetApprentice(Guid apprenticeId)
        {
            try
            {
                return await _client.GetApprentice(apprenticeId);
            }
            catch (ApiException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
    */
}
