using SFA.DAS.ApprenticePortal.Authentication;
using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public sealed class Apprentice : IApprenticeAccount
    {
        public Guid ApprenticeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool TermsOfUseAccepted { get; set; }
        public bool IsPrivateBetaUser { get; set; }
    }
}
