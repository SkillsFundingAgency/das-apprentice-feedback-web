using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Services
{
    /// <summary>
    /// We need to have an implementation of <code>IApprenticeAccountProvider</code> even 
    /// though we don't actually need to use it.
    /// </summary>
    public class ApprenticeAccountProvider : IApprenticeAccountProvider
    {
        public async Task<IApprenticeAccount> GetApprenticeAccount(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
