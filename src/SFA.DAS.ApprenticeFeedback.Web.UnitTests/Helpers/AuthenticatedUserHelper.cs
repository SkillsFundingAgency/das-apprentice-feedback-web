using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Security.Claims;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Helpers
{
    public static class AuthenticatedUserHelper
    {
        public static AuthenticatedUser CreateAuthenticatedUser(Guid apprenticeId)
        {
            var claimsPrincipal = new ClaimsPrincipal(new[]
            {
                new ClaimsIdentity(new[]
                {
                    new Claim(IdentityClaims.ApprenticeId, apprenticeId.ToString()),
                    new Claim(IdentityClaims.LogonId, Guid.NewGuid().ToString()),
                    new Claim(IdentityClaims.Name, "apprentice_feedback_esfa@mailinator.com"),
                }, "Test")
            });
            return new AuthenticatedUser(claimsPrincipal);
        }
    }
}
