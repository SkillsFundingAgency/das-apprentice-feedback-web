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
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        [Test, MoqAutoData]
        public async Task And_FeedbackRequestIsAvailable_SubmitsFeedbackForSignedInUser(FeedbackRequest request, Guid apprenticeId)
        {
            var user = AuthenticatedUserHelper.CreateAuthenticatedUser(apprenticeId);
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);

            var result = await CheckYourAnswersPage.OnPost(user);

            _mockFeedbackService.Verify(s => s.SubmitFeedback(It.Is<PostSubmitFeedback>(x =>
            x.Ukprn == request.Ukprn && x.ProviderName == request.TrainingProvider && x.StandardReference == request.StandardReference &&
            x.StandardUId == request.StandardUId && x.ApprenticeId == user.ApprenticeId && x.FeedbackAttributes == request.FeedbackAttributes)));

            result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be("Complete");
        }
    }
}
