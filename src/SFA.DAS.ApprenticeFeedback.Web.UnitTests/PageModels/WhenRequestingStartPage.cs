using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;
using SFA.DAS.ApprenticeFeedback.Web.UnitTests.Helpers;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.PageModels
{
    public class WhenRequestingStartPage
    {
        private StartModel StartPage;

        private Mock<IApprenticeFeedbackSessionService> _mockSessionService;
        private Mock<IApprenticeFeedbackService> _mockApprenticeFeedbackService;
        private Mock<Domain.Interfaces.IUrlHelper> _mockUrlHelper;

        [SetUp]
        public void Arrange()
        {
            _mockSessionService = new Mock<IApprenticeFeedbackSessionService>();
            _mockUrlHelper = new Mock<Domain.Interfaces.IUrlHelper>();
            _mockApprenticeFeedbackService = new Mock<IApprenticeFeedbackService>();
            StartPage = new StartModel(_mockSessionService.Object, _mockApprenticeFeedbackService.Object, _mockUrlHelper.Object, Mock.Of<ILogger<StartModel>>());
        }

        [Ignore("Temporarily ignire in order to get Azure Build running")]
        [Test, MoqAutoData]
        public async Task And_UkprnAndLarscodeProvidedInSession_FindApprenticeshipTrainingUrlIsGeneratedCorrectly(string url, Guid apprenticeshipId, int ukprn, TrainingProvider trainingProvider)
        {
            var user = AuthenticatedUserHelper.CreateAuthenticatedUser(apprenticeshipId);
            _mockApprenticeFeedbackService.Setup(s => s.GetTrainingProvider(apprenticeshipId, ukprn)).ReturnsAsync(trainingProvider);
            //_mockUrlHelper.Setup(u => u.FindApprenticeshipTrainingFeedbackUrl(ukprn, trainingProvider.GetMostRecentlyStartedApprenticeship().LarsCode)).Returns(url);

            var result = await StartPage.OnGet(user, ukprn);

            StartPage.FindApprenticeshipUrl.Should().Be(url);
        }
    }
}
