using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey
{
    public class ApprenticeExitSurvey
    {
        public Guid Id { get; set; }
        public DateTime DateTimeCompleted { get; set; }
        public bool DidNotCompleteApprenticeship { get; set; }
    }
}
