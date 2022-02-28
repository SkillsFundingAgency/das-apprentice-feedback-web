using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IApprenticeFeedbackService
    {
        Task SubmitFeedback(PostSubmitFeedback request);
    }
}
