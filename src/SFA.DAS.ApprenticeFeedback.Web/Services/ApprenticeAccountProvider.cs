using RestEase;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Services
{
    public class ApprenticeAccountProvider : IApprenticeAccountProvider
    {
        private readonly IApprenticeFeedbackApi _client;

        public ApprenticeAccountProvider(IApprenticeFeedbackApi client)
        {
            _client = client;
        }
        public async Task<IApprenticeAccount?> GetApprenticeAccount(Guid id)
        {
            try
            {
                return await _client.GetApprentice(id);
            }
            catch (ApiException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IApprenticeAccount> PutApprenticeAccount(string email, string govIdentifier)
        {
            return await _client.PutApprentice(new PutApprenticeRequest(email, govIdentifier));
        }
    }
}
