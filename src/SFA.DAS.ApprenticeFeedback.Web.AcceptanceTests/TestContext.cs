using SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Hooks;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests
{
    public class TestContext
    {
        public ApprenticeFeedbackWeb Web { get; set; }
        public MockOuterApi OuterApi { get; set; }
        public TestActionResult ActionResult { get; set; }
        public string IdentityServiceUrl { get; } = "https://identity";
    }

    public class RegisteredUserContext
    {
        public Guid ApprenticeId { get; set; } = Guid.NewGuid();
    }
}
