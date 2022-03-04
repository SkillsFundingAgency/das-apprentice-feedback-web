using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class ApprenticeFeedbackTarget
    {
        public Guid ApprenticeId { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<FeedbackResult> Results { get; set; }
    }
}
