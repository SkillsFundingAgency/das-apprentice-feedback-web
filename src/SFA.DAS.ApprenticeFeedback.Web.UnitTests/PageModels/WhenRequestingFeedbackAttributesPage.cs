using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.PageModels
{
    public class WhenRequestingFeedbackAttributesPage
    {
        private FeedbackAttributesModel FeedbackAttributesPage;

        private Mock<IApprenticeFeedbackSessionService> _mockSessionService;

        [SetUp]
        public void Arrange()
        {
            _mockSessionService = new Mock<IApprenticeFeedbackSessionService>();
            FeedbackAttributesPage = new FeedbackAttributesModel(_mockSessionService.Object);
        }

        [Ignore("Needs to be moved to start page or confirmed here using the FeedbackContext")]
        [Test, MoqAutoData]
        public async Task And_FeedbackAttributesIsNull_FeedbackAttributesIsPopulatedCorrectly(List<FeedbackSurveyAttribute> attributes)
        {
            //_mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);
            //request.FeedbackAttributes = null;
            //_mockFeedbackService.Setup(s => s.GetTrainingProviderAttributes()).ReturnsAsync(attributes);

            FeedbackAttributesPage.OnGet(true);

            FeedbackAttributesPage.FeedbackAttributes.Should().BeEquivalentTo(attributes);

        }

        [Ignore("Needs to be moved to start page or confirmed here using the FeedbackContext")]
        [Test, MoqAutoData]
        public async Task And_FeedbackAttributesIsPopulated_FeedbackAttributesIsPopulatedCorrectly(List<FeedbackSurveyAttribute> feedbackAttributes)
        {
            //_mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);
            //feedbackAttributes = request.FeedbackAttributes;

            //await FeedbackAttributesPage.OnGet(true);

            FeedbackAttributesPage.FeedbackAttributes.Should().BeEquivalentTo(feedbackAttributes);

        }
    }
}
