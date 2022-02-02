using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Models.OuterApi
{
    public class Apprentice : IApprenticeAccount
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool TermsOfUseAccepted { get; set; }
    }
}
