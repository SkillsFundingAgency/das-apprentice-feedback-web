using Microsoft.AspNetCore.Mvc;
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
                        new EngagementLink { Name = "TestLinkA", Url = "http://example-a.com/page1" },
                        new EngagementLink { Name = "TestLinkB", Url = "http://example-b.com/page1/page2" },
                        new EngagementLink { Name = "TestLinkE", Url = "http://example-e.com/page3?someParameter=someValue" },
                        new EngagementLink { Name = "TestLinkF", Url = "http://example-f.com?someParameter=someValue" },
                        new EngagementLink { Name = "TestLinkG", Url = "http://example-g.com" }
                }
            };
            
            _linksModel = new LinksModel(_mockEventPublisher.Object, _mockLogger.Object, _appSettings);
        }

        //[TestCase("TestLinkA", "appStart", "http://example-a.com/page1", 101)]
        //[TestCase("TestLinkB", "appWelcome", "http://example-b.com/page1/page2", 202)]
        //[TestCase("TestLinkE", "appStart", "http://example-e.com/page3?someParameter=someValue", 303)]
        //[TestCase("TestLinkF", "appStart", "http://example-f.com?someParameter=someValue", 404)]
        //[TestCase("TestLinkG", "appWelcome", "http://example-g.com", 505)]
        //public async Task WhenValidLinkNamePassed_EventIsPublished(string linkName, string templateName, string linkUrl, long feedbackTransactionId)
        //{
        //    // Arrange
        //    var apprenticeFeedbackTargetId = Guid.NewGuid();

        //    // Act
        //    _linksModel.TemplateName = templateName;
        //    var result = await _linksModel.OnGet(linkName, feedbackTransactionId, apprenticeFeedbackTargetId);

       // // Assert
       //_mockEventPublisher.Verify(p => p.Publish(It.Is<ApprenticeEmailClickEvent>(
       //    p => p.FeedbackTransactionId == feedbackTransactionId &&

       //    p.ApprenticeFeedbackTargetId == apprenticeFeedbackTargetId &&

       //    p.Linkname == linkName && 

       //    p.Link == linkUrl), 
       //     It.IsAny<PublishOptions>()), Times.Once);
        //}

        [TestCase("TestLinkA", "appStart", "http://example-a.com/page1?utm_source=apprentice_feedback&utm_medium=email&utm_campaign=engagement&utm_content=template_appStart", 101)]
        [TestCase("TestLinkA", "", "http://example-a.com/page1", 101)]
        [TestCase("TestLinkA", null, "http://example-a.com/page1", 101)]
        [TestCase("TestLinkB", "appWelcome", "http://example-b.com/page1/page2?utm_source=apprentice_feedback&utm_medium=email&utm_campaign=engagement&utm_content=template_appWelcome", 202)]
        [TestCase("TestLinkB", "", "http://example-b.com/page1/page2", 202)]
        [TestCase("TestLinkB", null, "http://example-b.com/page1/page2", 202)]
        [TestCase("TestLinkE", "appMonth3", "http://example-e.com/page3?someParameter=someValue&utm_source=apprentice_feedback&utm_medium=email&utm_campaign=engagement&utm_content=template_appMonth3", 303)]
        [TestCase("TestLinkE", "", "http://example-e.com/page3?someParameter=someValue", 303)]
        [TestCase("TestLinkE", null, "http://example-e.com/page3?someParameter=someValue", 303)]
        [TestCase("TestLinkF", "appStart", "http://example-f.com?someParameter=someValue&utm_source=apprentice_feedback&utm_medium=email&utm_campaign=engagement&utm_content=template_appStart", 404)]
        [TestCase("TestLinkF", "", "http://example-f.com?someParameter=someValue", 404)]
        [TestCase("TestLinkF", null, "http://example-f.com?someParameter=someValue", 404)]
        [TestCase("TestLinkG", "appWelcome", "http://example-g.com?utm_source=apprentice_feedback&utm_medium=email&utm_campaign=engagement&utm_content=template_appWelcome", 505)]
        [TestCase("TestLinkG", "", "http://example-g.com", 505)]
        [TestCase("TestLinkG", null, "http://example-g.com", 505)]
        public async Task WhenValidLinkNamePassed_RedirectToLinkUrlOccurs(string linkName, string templateName, string expectedUrl, long feedbackTransactionId)
        {
            // Arrange
            var apprenticeFeedbackTargetId = Guid.NewGuid();

            // Act
            _linksModel.TemplateName = templateName;
            var result = await _linksModel.OnGet(linkName, feedbackTransactionId, apprenticeFeedbackTargetId);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectResult>());
            var redirectResult = result as RedirectResult;
            Assert.That(expectedUrl,Is.EqualTo(redirectResult.Url));
        }

        [TestCase("TestLinkC", "appStart")]
        [TestCase("TestLinkD", "appWelcome")]
        public async Task WhenInvalidLinkNamePassedWarningIsLogged(string linkName, string templateName)
        {
            // Act
            _linksModel.TemplateName = templateName;
            var result = await _linksModel.OnGet(linkName, 1, Guid.NewGuid());

            // Assert
            _mockLogger.Verify(l => l.Log(LogLevel.Warning, 
                It.IsAny<EventId>(), 
                It.Is<object>(o => o != null), 
                It.IsAny<Exception>(), 
                (Func<object, Exception, string>)It.IsAny<object>()), 
                Times.Once);
        }

        [TestCase("TestLinkC", "appStart")]
        [TestCase("TestLinkD", "appWelcome")]
        public async Task WhenInvalidLinkNamePassedPageReturned(string linkName, string templateName)
        {
            // Act
            _linksModel.TemplateName = templateName;
            var result = await _linksModel.OnGet(linkName, 1, Guid.NewGuid());

            // Assert
            Assert.That(result, Is.TypeOf<PageResult>());
        }


        //[TestCase("TestLinkA", "appStart", 101)]
        //[TestCase("TestLinkB", "appWelcome", 202)]
        //public void WhenExceptionOccurs_ErrorIsLogged(string linkName, string templateName, long feedbackTransactionId)
        //{
        //    // Arrange
        //    _mockEventPublisher.Setup(p => p.Publish(It.IsAny<ApprenticeEmailClickEvent>(), It.IsAny<PublishOptions>())).ThrowsAsync(new Exception("Test exception"));

        //    // Act & Assert
        //    _linksModel.TemplateName = templateName;
        //    Assert.ThrowsAsync<Exception>(async () => await _linksModel.OnGet(linkName, feedbackTransactionId, Guid.NewGuid()));
        //    _mockLogger.Verify(l => l.Log(LogLevel.Error,
        //        It.IsAny<EventId>(),
        //        It.Is<object>(o => o != null),
        //        It.IsAny<Exception>(),
        //        (Func<object, Exception, string>)It.IsAny<object>()),
        //        Times.Once);
        //}
    }
}