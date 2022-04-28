using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Services
{
    public class ApprenticeAccountProvider : IApprenticeAccountProvider
    {
        //private readonly ApprenticeFeedbackApiClient _client;

        public ApprenticeAccountProvider(/*ApprenticeFeedbackApiClient client*/)
        {
            //_client = client;
        }

        public async Task<IApprenticeAccount> GetApprenticeAccount(Guid id)
        {
            //return await _client.TryGetApprentice(id);

            throw new NotImplementedException();   // TBC - we don't actually use this?
        }
    }
}
