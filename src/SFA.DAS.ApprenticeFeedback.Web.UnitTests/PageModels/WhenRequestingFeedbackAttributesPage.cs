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
    public class WhenRequestingFeedbackAttributesPage
    {
        private FeedbackAttributesModel FeedbackAttributesPage;

        private Mock<IApprenticeFeedbackSessionService> _mockSessionService;
        private Mock<IApprenticeFeedbackService> _mockFeedbackService;

        [SetUp]
        public void Arrange()
        {
            _mockSessionService = new Mock<IApprenticeFeedbackSessionService>();
            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
            FeedbackAttributesPage = new FeedbackAttributesModel(_mockSessionService.Object, _mockFeedbackService.Object);
        }

        [Test, MoqAutoData]
        public async Task And_FeedbackAttributesIsNull_FeedbackAttributesIsPopulatedCorrectly(FeedbackRequest request, Task<List<FeedbackAttribute>> attributes)
        {
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);
            _mockFeedbackService.Setup(s => s.GetTrainingProviderAttributes()).Returns(attributes);
            request.FeedbackAttributes = null;

            await FeedbackAttributesPage.OnGet(true);

            FeedbackAttributesPage.FeedbackAttributes.Should().AllBeEquivalentTo(attributes);
        }

        // ^^^ The code being tested:
        //var attributes = await _apprenticeFeedbackService.GetTrainingProviderAttributes();
        //feedbackRequest.FeedbackAttributes = attributes;
        //        _sessionService.UpdateFeedbackRequest(feedbackRequest);

        [Test, MoqAutoData]
        public async Task And_FeedbackAttributesIsPopulated_FeedbackAttributesIsPopulatedCorrectly(List<FeedbackAttribute> feedbackAttributes, FeedbackRequest request)
        {
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);
            feedbackAttributes = request.FeedbackAttributes;

            await FeedbackAttributesPage.OnGet(true);

            FeedbackAttributesPage.FeedbackAttributes.Should().BeEquivalentTo(feedbackAttributes);

        }

        //[Test, MoqAutoData]
        //public async Task And_PostingInvalidData_RefreshPage()
        //{

        //}

        //[Test, MoqAutoData]
        //public async Task And_PostingValidData_AndEditingIsTrue_RedirectToCheckAnswers()
        //{

        //}

        //[Test, MoqAutoData]
        //public async Task And_PostingValidData_AndEditingIsNotTrue_RedirectToOverallRating()
        //{

        //}
    }
}
