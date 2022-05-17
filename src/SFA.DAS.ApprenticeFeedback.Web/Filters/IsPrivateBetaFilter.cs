using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Filters
{
    public class IsPrivateBetaFilter : IAuthorizationFilter
    {

        private readonly NavigationUrlHelper _urlHelper;
        private readonly AuthenticatedUser _user;

        public IsPrivateBetaFilter(NavigationUrlHelper urlHelper, AuthenticatedUser user)
        {
            _urlHelper = urlHelper;
            _user = user;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_user.HasEnrolledInPrivateBeta)
                context.Result = new RedirectResult(_urlHelper.Generate(NavigationSection.Home, page: "Home"));
        }

    }
}
