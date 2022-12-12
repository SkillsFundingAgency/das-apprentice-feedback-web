using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Requests
{
    public class PostSubmitFeedback
    {
        public Guid ApprenticeFeedbackTargetId { get; set; }
        public OverallRating OverallRating { get; set; }
        public List<FeedbackSurveyAttribute> FeedbackAttributes { get; set; }
        public bool AllowContact { get; set; }
    }
}
