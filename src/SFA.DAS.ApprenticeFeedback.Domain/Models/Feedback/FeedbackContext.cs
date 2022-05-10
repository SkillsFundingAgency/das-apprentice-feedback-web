using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class FeedbackContext
    {
        public string ProviderName { get; set; }
        public long UkPrn { get; set; }
        public int LarsCode { get; set; }
        public Guid ApprenticeFeedbackTargetId { get; set; }
        public List<FeedbackAttribute> FeedbackAttributes { get; set; }
        public OverallRating? OverallRating { get; set; }

        public FeedbackEligibility FeedbackEligibility { get; set; }
        public DateTime? SignificantDate { get; set; }
        public TimeSpan? TimeWindow { get; set; }

        public FeedbackContext() { }

        public static FeedbackContext CreateFrom(TrainingProvider provider)
        {
            var feedbackContext = new FeedbackContext();
            if(null != provider)
            {
                feedbackContext.ProviderName = provider.Name;
                feedbackContext.UkPrn = provider.Ukprn;
                feedbackContext.LarsCode = provider.LarsCode;
                feedbackContext.FeedbackEligibility = FeedbackEligibility.Allow;

                // Needed so we know which target we're saving the feedback against
                // To save us re-calculating if there is more than one against each provider.
                feedbackContext.ApprenticeFeedbackTargetId = provider.ApprenticeFeedbackTargetId;
            }

            return feedbackContext;
        }
    }
}
