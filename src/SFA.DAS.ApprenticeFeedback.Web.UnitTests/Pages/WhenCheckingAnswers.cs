using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Pages
{
    public class WhenCheckingAnswers
    {
        private Mock<IApprenticeFeedbackSessionService> _mockSession;
        private Mock<IApprenticeFeedbackService> _mockFeedbackService;

        [SetUp]
        public void Setup()
        {
            _mockSession = new Mock<IApprenticeFeedbackSessionService>();
            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
        }

        [Test]
        public void And_Feedback_Allowed_Then_Return_Page()
        {
            _mockSession.Setup(m => m.GetFeedbackContext()).Returns(
                new FeedbackContext()
                {
                    FeedbackEligibility = FeedbackEligibility.Allow
                });
            var model = new CheckYourAnswersModel(_mockSession.Object, _mockFeedbackService.Object);
            model.OnPageHandlerExecuting(null);

            var result = model.OnGet();

            result.Should().BeOfType<PageResult>();
        }

        [Test]
        public void And_Feedback_Not_Allowed_Then_Redirect_To_Home()
        {
            _mockSession.Setup(m => m.GetFeedbackContext()).Returns(
                new FeedbackContext()
                {
                    FeedbackEligibility = FeedbackEligibility.Deny_HasGivenFeedbackRecently
                });
            var model = new CheckYourAnswersModel(_mockSession.Object, _mockFeedbackService.Object);
            model.OnPageHandlerExecuting(null);

            var result = model.OnGet();

            result.Should().BeOfType<RedirectResult>();
            var redirect = result as RedirectResult;
            redirect.Url.Should().Be("/index");
        }
    }
}
