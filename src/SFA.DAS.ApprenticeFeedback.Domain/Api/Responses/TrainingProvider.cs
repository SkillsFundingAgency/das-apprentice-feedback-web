using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public enum FeedbackEligibility
    {
        Unknown = 0,
        Allow = 1,
        Deny_TooSoon = 2,
        Deny_TooLateAfterPassing = 3,
        Deny_TooLateAfterWithdrawing = 4,
        Deny_TooLateAfterPausing = 5,
        Deny_HasGivenFeedbackRecently = 6,
        Deny_HasGivenFinalFeedback = 7,
        Deny_NotEnoughActiveApprentices = 8,
        Deny_Complete = 9,
    }


    public class TrainingProvider
    {
        public Guid ApprenticeFeedbackTargetId { get; set; }
        public string ProviderName { get; set; }
        public long UkPrn { get; set; }
        public int LarsCode { get; set; }
        public DateTime? LastFeedbackSubmittedDate { get; set; }
        public FeedbackEligibility FeedbackEligibility { get; set; } 
        public TimeSpan? TimeWindow { get; set; }
        public DateTime? SignificantDate { get; set; }
    }
}
