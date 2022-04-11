using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using System;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
