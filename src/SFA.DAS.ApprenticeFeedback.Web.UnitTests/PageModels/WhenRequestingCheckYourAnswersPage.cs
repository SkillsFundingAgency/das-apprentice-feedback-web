using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.PageModels
{
    public class WhenRequestingCheckYourAnswersPage
    {
        private CheckYourAnswersModel CheckYourAnswersPage;

        private Mock<IApprenticeFeedbackSessionService> _mockSessionService;
        private Mock<IApprenticeFeedbackService> _mockFeedbackService;

        [SetUp]
        public void Arrange()
        {
            _mockSessionService = new Mock<IApprenticeFeedbackSessionService>();
            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
            CheckYourAnswersPage = new CheckYourAnswersModel(_mockSessionService.Object, _mockFeedbackService.Object);
        }

        [Test, MoqAutoData]
        public void And_FeedbackRequestIsAvailable_SetsModelCorrectly(FeedbackRequest request)
        {
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);
                        
            CheckYourAnswersPage.OnGet();

            CheckYourAnswersPage.TrainingProvider.Should().Be(request.TrainingProvider);
            CheckYourAnswersPage.OverallRating.Should().Be(request.OverallRating);
            CheckYourAnswersPage.FeedbackAttributes.Should().BeEquivalentTo(request.FeedbackAttributes);
        }
    }
}
