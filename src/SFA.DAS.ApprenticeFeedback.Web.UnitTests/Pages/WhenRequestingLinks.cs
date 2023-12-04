using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NServiceBus;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Settings;
using SFA.DAS.ApprenticeFeedback.Messages.Events;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Engagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Pages
{
    [TestFixture]
    public class WhenRequestingLinks
    {
        private Mock<IMessageSession> _mockEventPublisher;
        private Mock<ILogger<LinksModel>> _mockLogger;
        private AppSettings _appSettings;
        private LinksModel _linksModel;

        [SetUp]
        public void SetUp()
        {
            _mockEventPublisher = new Mock<IMessageSession>();
            _mockLogger = new Mock<ILogger<LinksModel>>();
            
            _appSettings = new AppSettings
            {
                EngagementLinks = new List<EngagementLink> 
                {
                        new EngagementLink { Name = "TestLinkA", Url = "http://exampleA.com" },
                        new EngagementLink { Name = "TestLinkB", Url = "http://exampleB.com" }
                }
            };
            
            _linksModel = new LinksModel(_mockEventPublisher.Object, _mockLogger.Object, _appSettings);
        }

        [TestCase("TestLinkA", "http://exampleA.com", 101)]
        [TestCase("TestLinkB", "http://exampleB.com", 202)]
        public async Task WhenValidLinkNamePassed_EventIsPublished(string linkName, string linkUrl, long feedbackTransactionId)
        {
            // Arrange
            var apprenticeFeedbackTargetId = Guid.NewGuid();

            // Act
            var result = await _linksModel.OnGet(linkName, feedbackTransactionId, apprenticeFeedbackTargetId);

            // Assert
            _mockEventPublisher.Verify(p => p.Publish(It.Is<ApprenticeEmailClickEvent>(
                p => p.FeedbackTransactionId == feedbackTransactionId &&
                p.ApprenticeFeedbackTargetId == apprenticeFeedbackTargetId &&
                p.Linkname == linkName && 
                p.Link == linkUrl), 
                It.IsAny<PublishOptions>()), Times.Once);
        }

        [TestCase("TestLinkA", "http://exampleA.com", 101)]
        [TestCase("TestLinkB", "http://exampleB.com", 202)]
        public async Task WhenValidLinkNamePassed_RedirectToLinkUrlOccurs(string linkName, string expectedUrl, long feedbackTransactionId)
        {
            // Arrange
            var apprenticeFeedbackTargetId = Guid.NewGuid();

            // Act
            var result = await _linksModel.OnGet(linkName, feedbackTransactionId, apprenticeFeedbackTargetId);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectResult>());
            var redirectResult = result as RedirectResult;
            Assert.AreEqual(expectedUrl, redirectResult.Url);
        }

        [TestCase("TestLinkC")]
        [TestCase("TestLinkD")]
        public async Task WhenInvalidLinkNamePassedWarningIsLogged(string linkName)
        {
            // Act
            var result = await _linksModel.OnGet(linkName, 1, Guid.NewGuid());

            // Assert
            _mockLogger.Verify(l => l.Log(LogLevel.Warning, 
                It.IsAny<EventId>(), 
                It.Is<object>(o => o != null), 
                It.IsAny<Exception>(), 
                (Func<object, Exception, string>)It.IsAny<object>()), 
                Times.Once);
        }

        [TestCase("TestLinkC")]
        [TestCase("TestLinkD")]
        public async Task WhenInvalidLinkNamePassedPageReturned(string linkName)
        {
            // Act
            var result = await _linksModel.OnGet(linkName, 1, Guid.NewGuid());

            // Assert
            Assert.That(result, Is.TypeOf<PageResult>());
        }


        [TestCase("TestLinkA", 101)]
        [TestCase("TestLinkB", 202)]
        public void WhenExceptionOccurs_ErrorIsLogged(string linkName, long feedbackTransactionId)
        {
            // Arrange
            _mockEventPublisher.Setup(p => p.Publish(It.IsAny<ApprenticeEmailClickEvent>(), It.IsAny<PublishOptions>())).ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _linksModel.OnGet(linkName, feedbackTransactionId, Guid.NewGuid()));
            _mockLogger.Verify(l => l.Log(LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<object>(o => o != null),
                It.IsAny<Exception>(),
                (Func<object, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}