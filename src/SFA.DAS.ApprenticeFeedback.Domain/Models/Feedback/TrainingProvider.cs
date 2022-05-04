using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class TrainingProvider
    {
        public Guid ApprenticeFeedbackTargetId { get; set; }
        public string Name { get; set; }
        public long Ukprn { get; set; }
        public FeedbackEligibility FeedbackEligibility { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public TimeSpan? TimeWindow { get; set; }
        public DateTime? SignificantDate { get; set; }
    }
}
