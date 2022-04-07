using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
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
        
        [OneTimeSetUp]
        public void Setup()
        {
            _mockApiClient = new Mock<IApprenticeFeedbackApi>();
            _apprenticeFeedbackService = new ApprenticeFeedbackService(_mockApiClient.Object);
        }


        [Test, MoqAutoData]
        public async Task When_CallingSubmitFeedback_Then_FeedbackSubmitted(
            PostSubmitFeedback request)
        {
            PostSubmitFeedback sentRequest = null;
            _mockApiClient.Setup(s => s.SubmitFeedback(It.IsAny<PostSubmitFeedback>())).Callback<PostSubmitFeedback>(x => sentRequest = x);

            await _apprenticeFeedbackService.SubmitFeedback(request);

            sentRequest.Should().BeEquivalentTo(request);
        }


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
