using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
