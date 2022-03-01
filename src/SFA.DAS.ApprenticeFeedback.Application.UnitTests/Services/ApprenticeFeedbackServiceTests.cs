using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;

namespace SFA.DAS.ApprenticeFeedback.Application.UnitTests.Services
{
    public class ApprenticeFeedbackServiceTests
    {
        private Mock<IApprenticeFeedbackApi> _mockApiClient;

        private ApprenticeFeedbackService _apprenticeFeedbackService;

        [SetUp]
        public void Arrange()
        {
            _mockApiClient = new Mock<IApprenticeFeedbackApi>();

            _apprenticeFeedbackService = new ApprenticeFeedbackService(_mockApiClient.Object);
        }

        // To do: Tests for submit feedback
    }
}
