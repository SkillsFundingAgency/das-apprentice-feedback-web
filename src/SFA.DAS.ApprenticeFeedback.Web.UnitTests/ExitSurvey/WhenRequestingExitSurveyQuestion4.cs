using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey;
using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.ExitSurvey
{
    public class WhenHandlingQuestion4
    {
        private Mock<IExitSurveySessionService> _mockSessionService;
        private Mock<IApprenticeFeedbackService> _mockApprenticeFeedbackService;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Question4Model _question4Model;
        private AuthenticatedUser _authenticatedUser;

        [SetUp]
        public void Arrange()
        {
            _mockSessionService = new Mock<IExitSurveySessionService>();
            _mockApprenticeFeedbackService = new Mock<IApprenticeFeedbackService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(CreateMockHttpContext());

            _authenticatedUser = new AuthenticatedUser(_mockHttpContextAccessor.Object);

            var mockExitSurveyContext = new ExitSurveyContext
            {
                ApprenticeFeedbackTargetId = Guid.NewGuid(),
                Attributes = new HashSet<ExitSurveyAttribute>(),
                CheckingAnswers = false,
                SurveyCompleted = false
            };

            _mockSessionService
                .Setup(s => s.GetExitSurveyContext())
                .Returns(mockExitSurveyContext);

            _question4Model = new Question4Model(_mockSessionService.Object, _mockApprenticeFeedbackService.Object);

            _question4Model.OnPageHandlerExecuting(CreatePageHandlerExecutingContext("/exit/question4"));
        }

        private HttpContext CreateMockHttpContext()
        {
            var claims = new List<Claim>
            {
                new Claim("apprentice_id", Guid.NewGuid().ToString()),
                new Claim("email", "test@example.com"),
                new Claim("has_created_account", "true"),
                new Claim("has_accepted_terms_of_use", "true")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            return httpContext;
        }

        private PageHandlerExecutingContext CreatePageHandlerExecutingContext(string path)
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

            pageHandlerExecutingContext.HttpContext.Request.Path = path;

            return pageHandlerExecutingContext;
        }

        [Test]
        public async Task OnGet_ShouldRetrieveAttributes_AndPreselectExistingOne()
        {
            var category = "PostApprenticeshipStatus";
            var expectedAttributes = new List<ExitSurveyAttribute>
            {
                new ExitSurveyAttribute { Id = 1, Category = category },
                new ExitSurveyAttribute { Id = 2, Category = category }
            };

            _mockApprenticeFeedbackService
                .Setup(s => s.GetExitSurveyAttributes(category))
                .ReturnsAsync(expectedAttributes);

            _question4Model.ExitSurveyContext.Attributes = new HashSet<ExitSurveyAttribute>
            {
                new ExitSurveyAttribute { Id = 1, Category = category }
            };

            var result = await _question4Model.OnGet(_authenticatedUser);

            result.Should().BeOfType<PageResult>();
            _question4Model.Attributes.Should().BeEquivalentTo(expectedAttributes);
            _question4Model.SelectedAttributeId.Should().Be(1);
        }

        [Test]
        public void OnPost_ShouldClearContext_AddSelectedAttribute_AndRedirect()
        {
            var selectedAttribute = new ExitSurveyAttribute { Id = 1 };

            _question4Model.Attributes = new List<ExitSurveyAttribute> { selectedAttribute };
            _question4Model.SelectedAttributeId = 1;
            _question4Model.ExitSurveyContext.Attributes = new HashSet<ExitSurveyAttribute>();

            var result = _question4Model.OnPost();

            result.Should().BeOfType<RedirectToPageResult>()
                    .Which.PageName.Should().Be("./checkyouranswers");

            _question4Model.ExitSurveyContext.Attributes.Should().Contain(selectedAttribute);
        }

        [Test]
        public void OnPost_ShouldReturnPageResult_WhenModelStateIsInvalid()
        {
            _question4Model.ModelState.AddModelError("SelectedAttributeId", "Error");

            var result = _question4Model.OnPost();

            result.Should().BeOfType<PageResult>();
        }
    }
}
