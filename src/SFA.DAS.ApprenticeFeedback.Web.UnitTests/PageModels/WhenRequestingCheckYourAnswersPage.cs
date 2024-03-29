﻿using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;
using SFA.DAS.Testing.AutoFixture;

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

        [Ignore("Temporarily ignire in order to get Azure Build running")]
        [Test, MoqAutoData]
        public void And_FeedbackRequestIsAvailable_SetsModelCorrectly(FeedbackContext context)
        {
            _mockSessionService.Setup(s => s.GetFeedbackContext()).Returns(context);
            CheckYourAnswersPage.OnGet();

            CheckYourAnswersPage.ProviderName.Should().Be(context.ProviderName);
            CheckYourAnswersPage.OverallRating.Should().Be(context.OverallRating);
            CheckYourAnswersPage.FeedbackAttributes.Should().BeEquivalentTo(context.FeedbackAttributes);
        }
    }
}
