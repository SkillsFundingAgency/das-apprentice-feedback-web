using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Helpers
{
    public static class AuthenticatedUserHelper
    {
        public static AuthenticatedUser CreateAuthenticatedUser(Guid apprenticeId)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var defaultContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new[]
                {
                    new ClaimsIdentity(new[]
                    {
                        new Claim(IdentityClaims.ApprenticeId, apprenticeId.ToString()),
                        new Claim(IdentityClaims.LogonId, Guid.NewGuid().ToString()),
                        new Claim(IdentityClaims.Name, "apprentice_feedback_esfa@mailinator.com"),
                    }, "Test")
                })
            };
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(defaultContext);
            return new AuthenticatedUser(mockHttpContextAccessor.Object);
        }
    }
}
