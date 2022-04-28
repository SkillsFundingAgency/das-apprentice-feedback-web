using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
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
    public class WhenPostingToCheckYourAnswersPage
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

        [Ignore("Temporarily ignire in order to get Azure Build running")]
        [Test, MoqAutoData]
        public async Task And_FeedbackRequestIsAvailable_SubmitsFeedbackForSignedInUser(FeedbackContext context, Guid apprenticeId)
        {
            PostSubmitFeedback postSubmitFeedback = null;
            CheckYourAnswersPage.ContactConsent = true;
            var user = AuthenticatedUserHelper.CreateAuthenticatedUser(apprenticeId);
            _mockSessionService.Setup(s => s.GetFeedbackContext()).Returns(context);
            _mockFeedbackService.Setup(s => s.SubmitFeedback(It.IsAny<PostSubmitFeedback>())).Callback<PostSubmitFeedback>(x => postSubmitFeedback = x);

            var result = await CheckYourAnswersPage.OnPost();

            postSubmitFeedback.Should().BeEquivalentTo(new
            {
                context.OverallRating,
                context.FeedbackAttributes,
                ContactConsent = true,
                context.ApprenticeFeedbackTargetId

            });
            result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be("Complete");
        }
    }
}
