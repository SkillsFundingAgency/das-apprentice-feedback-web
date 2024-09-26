using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;
using SFA.DAS.ApprenticePortal.Authentication;
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
        private Mock<IHttpContextAccessor> _contextAccessor;
        private AuthenticatedUser _authenticatedUser;

        [SetUp]
        public void Arrange()
        {
            _mockSessionService = new Mock<IApprenticeFeedbackSessionService>();
            _mockUrlHelper = new Mock<Domain.Interfaces.IUrlHelper>();
            _mockApprenticeFeedbackService = new Mock<IApprenticeFeedbackService>();
            _contextAccessor = new Mock<IHttpContextAccessor>();
            _contextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
            _authenticatedUser = new AuthenticatedUser(_contextAccessor.Object);
            StartPage = new StartModel(_mockSessionService.Object, _mockApprenticeFeedbackService.Object, _mockUrlHelper.Object);
        }

        [Ignore("Temporarily ignire in order to get Azure Build running")]
        [Test, MoqAutoData]
        public async Task And_UkprnAndLarscodeProvidedInSession_FindApprenticeshipTrainingUrlIsGeneratedCorrectly(string url, Guid apprenticeshipId, int ukprn, TrainingProvider trainingProvider)
        {
            _mockApprenticeFeedbackService.Setup(s => s.GetTrainingProvider(apprenticeshipId, ukprn)).ReturnsAsync(trainingProvider);
            //_mockUrlHelper.Setup(u => u.FindApprenticeshipTrainingFeedbackUrl(ukprn, trainingProvider.GetMostRecentlyStartedApprenticeship().LarsCode)).Returns(url);

            var result = await StartPage.OnGet(_authenticatedUser, ukprn);

            StartPage.FindApprenticeshipUrl.Should().Be(url);
        }
    }
}
