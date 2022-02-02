using RestEase;
using SFA.DAS.ApprenticeFeedback.Web.Models.OuterApi;
using SFA.DAS.ApprenticeFeedback.Web.Services.OuterApi;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Services
{
    public class ApprenticeFeedbackApiClient
    {
        private readonly IOuterApiClient _client;

        public ApprenticeFeedbackApiClient(IOuterApiClient client)
        {
            _client = client;
        }

        public async Task<Apprentice?> TryGetApprentice(Guid apprenticeId)
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
}
