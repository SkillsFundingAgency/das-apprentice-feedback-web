using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Web.UnitTests.Helpers;
using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Pages
{
    public class WhenStartingFeedbackJourney
    {
        private Mock<IExitSurveySessionService> _mockSession;
        private Mock<IApprenticeFeedbackService> _mockFeedbackService;
        private StartModel _startPage;

        private AuthenticatedUser _authenticatedUser;
        private Guid _apprenticeFeedbackTargetId;

        [SetUp]
        public void Arrange()
        {
            _mockSession = new Mock<IExitSurveySessionService>();
            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
            _startPage = new StartModel(_mockSession.Object, _mockFeedbackService.Object);
            _startPage.OnPageHandlerExecuting(CreatePageHandlerExecutingContext("/exit/start"));

            _authenticatedUser = AuthenticatedUserHelper.CreateAuthenticatedUser(Guid.NewGuid());
            _apprenticeFeedbackTargetId = Guid.NewGuid();
        }

        private PageHandlerExecutingContext CreatePageHandlerExecutingContext(string contextUrlPath)
        {
            var pageContext = new PageContext(new ActionContext(
                            new DefaultHttpContext(),
                            new RouteData(),
                            new PageActionDescriptor(),
                            new ModelStateDictionary()));
            var pageHandlerExecutingContext = new PageHandlerExecutingContext(
               pageContext,
               Array.Empty<IFilterMetadata>(),
               new HandlerMethodDescriptor(),
               new Dictionary<string, object>(),
               new object());

            pageHandlerExecutingContext.HttpContext.Request.Path = contextUrlPath;

            return pageHandlerExecutingContext;
        }

        [Test]
        public async Task And_ApprenticeHasNotWithdrawn_Then_ShowIndexPage()
        {
            _mockFeedbackService.Setup(m => m.GetApprenticeFeedbackTargets(It.IsAny<Guid>()))
                .ReturnsAsync(new List<ApprenticeFeedbackTarget>()
                {
                    new ApprenticeFeedbackTarget()
                    {
                        Id = _apprenticeFeedbackTargetId,
                        ApprenticeId = _authenticatedUser.ApprenticeId,
                        Withdrawn = false
                    }
                });

            var result = await _startPage.OnGet(_authenticatedUser, _apprenticeFeedbackTargetId);

            result.Should().BeOfType<RedirectResult>();
            var redirectResult = result as RedirectResult;
            redirectResult.Url.Should().Be("/");
        }

        [Test]
        public async Task And_ApprenticeHasWithdrawn_Then_ShowStartPage()
        {
            _mockFeedbackService.Setup(m => m.GetApprenticeFeedbackTargets(It.IsAny<Guid>()))
                .ReturnsAsync(new List<ApprenticeFeedbackTarget>()
                {
                    new ApprenticeFeedbackTarget()
                    {
                        Id = _apprenticeFeedbackTargetId,
                        ApprenticeId = _authenticatedUser.ApprenticeId,
                        Withdrawn = true
                    }
                });

            var result = await _startPage.OnGet(_authenticatedUser, _apprenticeFeedbackTargetId);

            result.Should().BeOfType<PageResult>();
        }
    }
}
