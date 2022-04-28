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
                feedbackContext.FeedbackEligibility = FeedbackEligibility.Allow;

                // Needs sorting with the singular call via ukprn plus sign in id
                feedbackContext.ApprenticeFeedbackTargetId = new Guid("07B3C9BC-07BA-4F48-823D-EA07933CB922");
            }

            return feedbackContext;
        }
    }
}
