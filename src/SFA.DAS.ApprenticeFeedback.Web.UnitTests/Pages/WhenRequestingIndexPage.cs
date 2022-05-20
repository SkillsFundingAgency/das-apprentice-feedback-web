using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages;
using SFA.DAS.ApprenticeFeedback.Web.UnitTests.Helpers;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Pages
{
    public class WhenRequestingIndexPage
    {
        private IndexModel IndexPage;

        private Mock<IApprenticeFeedbackService> _mockFeedbackService;
        private Mock<IApprenticeFeedbackSessionService> _mockSession;
        private NavigationUrlHelper _urlHelper;
        private AuthenticatedUser _authenticatedUser;

        [SetUp]
        public void Arrange()
        {
            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
            _mockSession = new Mock<IApprenticeFeedbackSessionService>();

            var urls = new NavigationSectionUrls();
            urls.ApprenticeHomeUrl = new Uri("https://localhost:5001/");
            _urlHelper = new NavigationUrlHelper(urls);

            _authenticatedUser = AuthenticatedUserHelper.CreateAuthenticatedUser(Guid.NewGuid());

            IndexPage = new IndexModel(Mock.Of<ILogger<IndexModel>>(), _mockFeedbackService.Object, _urlHelper, _mockSession.Object);
        }

        [Test]
        public async Task And_MultipleTrainingProvidersReturned_Then_ShowIndexPage()
        {
            _mockFeedbackService.Setup(m => m.GetTrainingProviders(It.IsAny<Guid>()))
                .ReturnsAsync(new List<TrainingProvider>()
                {
                    new TrainingProvider()
                    {
                        Ukprn = 12345678,
                        Name = "First Training Provider",
                        FeedbackEligibility = FeedbackEligibility.Allow
                    },
                    new TrainingProvider()
                    {
                        Ukprn = 87654321,
                        Name = "Second Training Provider",
                        FeedbackEligibility = FeedbackEligibility.Allow
                    }
                });

            var result = await IndexPage.OnGet(_authenticatedUser);

            result.Should().BeOfType<PageResult>();
        }

        [Test]
        public async Task And_SingleProviderReturned_And_FeedbackAllowed_Then_RedirectToStartPage()
        {
            _mockFeedbackService.Setup(m => m.GetTrainingProviders(It.IsAny<Guid>()))
                .ReturnsAsync(new List<TrainingProvider>() 
                { 
                    new TrainingProvider() 
                    { 
                        Ukprn = 12345678,
                        Name = "Single Training Provider",
                        FeedbackEligibility = FeedbackEligibility.Allow
                    }
                });

            var result = await IndexPage.OnGet(_authenticatedUser);

            result.Should().BeOfType<RedirectResult>();
            var pageResult = (RedirectResult)result;

            pageResult.Url.Should().Be("/start/12345678");
        }

        [Test]
        public async Task And_SingleProviderReturned_And_FeedbackNotAllowed_Then_RedirectToStatusPage()
        {
            _mockFeedbackService.Setup(m => m.GetTrainingProviders(It.IsAny<Guid>()))
                .ReturnsAsync(new List<TrainingProvider>()
                {
                    new TrainingProvider()
                    {
                        Ukprn = 12345678,
                        Name = "Single Training Provider",
                        FeedbackEligibility = FeedbackEligibility.Deny_HasGivenFeedbackRecently
                    }
                });

            var result = await IndexPage.OnGet(_authenticatedUser);

            result.Should().BeOfType<RedirectResult>();
            var pageResult = (RedirectResult)result;

            pageResult.Url.Should().Be("/status");
        }
    }
}
