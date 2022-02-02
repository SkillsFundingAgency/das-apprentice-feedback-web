using RestEase;
using SFA.DAS.ApprenticeFeedback.Web.Models.OuterApi;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Services.OuterApi
{
    public interface IOuterApiClient
    {
        [Get("/apprentices/{id}")]
        Task<Apprentice> GetApprentice([Path] Guid id);
    }
}
