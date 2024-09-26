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
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.Testing.AutoFixture;
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
        private Mock<IHttpContextAccessor> _contextAccessor;
        private AuthenticatedUser _authenticatedUser;

        private StartModel _startPage;

        [SetUp]
        public void Arrange()
        {
            _mockSession = new Mock<IExitSurveySessionService>();
            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
            _startPage = new StartModel(_mockSession.Object, _mockFeedbackService.Object);
            _startPage.OnPageHandlerExecuting(CreatePageHandlerExecutingContext("/exit/start"));
            _contextAccessor = new Mock<IHttpContextAccessor>();
            _contextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
            _authenticatedUser = new AuthenticatedUser(_contextAccessor.Object);

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

        [Test, MoqAutoData]
        public async Task And_ApprenticeHasNotWithdrawn_Then_ShowIndexPage(Guid apprenticeFeedbackTargetId)
        {
            _mockFeedbackService.Setup(m => m.GetApprenticeFeedbackTargets(It.IsAny<Guid>()))
                .ReturnsAsync(new List<ApprenticeFeedbackTarget>()
                {
                    new ApprenticeFeedbackTarget()
                    {
                        Id = apprenticeFeedbackTargetId,
                        ApprenticeId = _authenticatedUser.ApprenticeId,
                        Withdrawn = false
                    }
                });

            var result = await _startPage.OnGet(_authenticatedUser, apprenticeFeedbackTargetId);

            result.Should().BeOfType<RedirectResult>();
            var redirectResult = result as RedirectResult;
            redirectResult.Url.Should().Be("/");
        }

        [Test, MoqAutoData]
        public async Task And_ApprenticeHasWithdrawn_Then_ShowStartPage(Guid apprenticeFeedbackTargetId)
        {
            _mockFeedbackService.Setup(m => m.GetApprenticeFeedbackTargets(It.IsAny<Guid>()))
                .ReturnsAsync(new List<ApprenticeFeedbackTarget>()
                {
                    new ApprenticeFeedbackTarget()
                    {
                        Id = apprenticeFeedbackTargetId,
                        ApprenticeId = _authenticatedUser.ApprenticeId,
                        Withdrawn = true
                    }
                });

            var result = await _startPage.OnGet(_authenticatedUser, apprenticeFeedbackTargetId);

            result.Should().BeOfType<PageResult>();
        }
    }
}
