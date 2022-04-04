using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Application.UnitTests.Services
{
    public class ApprenticeFeedbackServiceTests
    {
        private Mock<IApprenticeFeedbackApi> _mockApiClient;

        private ApprenticeFeedbackService _apprenticeFeedbackService;

        // To do: Tests for submit feedback


        [Test, MoqAutoData]
        public async Task When_CallingGetTrainingProviders_Then_GetTrainingProviders(
            Guid apprenticeId, 
            [Frozen] Mock<IApprenticeFeedbackApi> _mockApiClient,
            [Greedy] ApprenticeFeedbackService service,
            GetTrainingProvidersResponse response)
        {
            _mockApiClient.Setup(c => c.GetTrainingProviders(apprenticeId)).ReturnsAsync(response);

            var result = await service.GetTrainingProviders(apprenticeId);

            result.Should().BeEquivalentTo(response.TrainingProviders);
        }
    }
}
