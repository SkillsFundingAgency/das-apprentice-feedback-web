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
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Pages
{
    public class WhenInFeedbackJourney
    {
        private static Mock<IApprenticeFeedbackSessionService> _mockSession
            = new Mock<IApprenticeFeedbackSessionService>();
        private static Mock<IApprenticeFeedbackService> _mockFeedbackService
            = new Mock<IApprenticeFeedbackService>();
        private static Mock<Domain.Interfaces.IUrlHelper> _mockUrlHelper
            = new Mock<Domain.Interfaces.IUrlHelper>();
        private static NavigationUrlHelper _navigationUrlHelper
            = new NavigationUrlHelper(new NavigationSectionUrls());

        static object[] PageModelCases =
        {
            new object[] { new CheckYourAnswersModel(_mockSession.Object, _mockFeedbackService.Object) },
            new object[] { new CompleteModel(_mockSession.Object, _mockUrlHelper.Object, _navigationUrlHelper ) },
            new object[] { new FeedbackAttributesModel(_mockSession.Object) },
            new object[] { new OverallRatingModel(_mockSession.Object) },
        };

        [TestCaseSource(nameof(PageModelCases))]
        public void And_Session_Does_Not_Exist_Then_Redirect_To_Home(FeedbackContextPageModel model)
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
            model.OnPageHandlerExecuting(pageHandlerExecutingContext);

            pageHandlerExecutingContext.Result.Should().BeOfType<RedirectResult>();
            var redirect = pageHandlerExecutingContext.Result as RedirectResult;
            redirect.Url.Should().Be("/");
        }
    }
}
