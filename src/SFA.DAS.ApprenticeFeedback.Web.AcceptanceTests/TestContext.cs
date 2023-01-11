using System;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests
{
    public class TestContext
    {
        public ApprenticeFeedbackWeb Web { get; set; }
        public MockOuterApi OuterApi { get; set; }
        public string IdentityServiceUrl { get; } = "https://identity";
        public string TestActionResultContent { get; set; }
    }

    public class RegisteredUserContext
    {
        public Guid ApprenticeId { get; set; } = Guid.NewGuid();
    }
}
