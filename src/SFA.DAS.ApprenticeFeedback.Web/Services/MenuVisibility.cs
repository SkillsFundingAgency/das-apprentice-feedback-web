using SFA.DAS.ApprenticePortal.SharedUi.Services;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Services
{
    internal class MenuVisibility : IMenuVisibility
    {
        public async Task<bool> ShowConfirmMyApprenticeship() => await Task.FromResult(true);

        public async Task<bool> ShowApprenticeFeedback() => await Task.FromResult(true);

        public async Task<bool> ShowApprenticeAan() => await Task.FromResult(true);

        public async Task<ConfirmMyApprenticeshipTitleStatus> ConfirmMyApprenticeshipTitleStatus() =>
            await Task.FromResult(ApprenticePortal.SharedUi.Services.ConfirmMyApprenticeshipTitleStatus.ShowAsConfirmed);
    }
}