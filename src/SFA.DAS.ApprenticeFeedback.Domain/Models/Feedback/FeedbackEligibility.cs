namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
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
}
