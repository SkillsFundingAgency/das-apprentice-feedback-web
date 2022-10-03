using SFA.DAS.ApprenticePortal.SharedUi.Services;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Services
{
    internal class MenuVisibility : IMenuVisibility
    {
        public async Task<bool> ShowConfirmMyApprenticeship() => true;

        public async Task<bool> ShowApprenticeFeedback() => true;

        public async Task<ConfirmMyApprenticeshipTitleStatus> ConfirmMyApprenticeshipTitleStatus() =>
            ApprenticePortal.SharedUi.Services.ConfirmMyApprenticeshipTitleStatus.ShowAsConfirmed;
    }
}