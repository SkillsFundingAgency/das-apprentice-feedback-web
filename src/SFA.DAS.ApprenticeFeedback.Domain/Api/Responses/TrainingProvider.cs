using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public enum FeedbackEligibility
    {
        Allow,
        Deny_TooSoon,
        Deny_TooLateAfterPassing,
        Deny_TooLateAfterWithdrawing,
        Deny_HasGivenFeedbackRecently,
        Deny_HasGivenFinalFeedback
    }


    public class TrainingProvider
    {
        public string ProviderName { get; set; }
        public long UkPrn { get; set; }
        public DateTime? LastFeedbackSubmittedDate { get; set; }
        public FeedbackEligibility FeedbackEligibility { get; set; } 
        public TimeSpan? TimeWindow { get; set; }
        public DateTime? SignificantDate { get; set; }
    }
}
