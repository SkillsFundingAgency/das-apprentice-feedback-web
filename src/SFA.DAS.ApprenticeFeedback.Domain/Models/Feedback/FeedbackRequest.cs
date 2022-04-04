using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class FeedbackRequest
    {
        // temporary test name
        public string TrainingProvider { get; set; } = "Temporary Test Provider name";
        public long Ukprn { get; set; }
        public int LarsCode { get; set; }

        public List<FeedbackAttribute> FeedbackAttributes { get; set; } 
        public OverallRating? OverallRating { get; set; }

    }
}
