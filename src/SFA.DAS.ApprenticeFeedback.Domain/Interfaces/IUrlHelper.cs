using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IUrlHelper
    {
        public string FATFeedback(long ukprn, int larscode);
    }
}
