using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Requests
{
    public class PostSubmitFeedback
    {
        public Guid ApprenticeId { get; set; }
        public long Ukprn { get; set; }
        public OverallRating OverallRating { get; set; }
        public List<FeedbackAttribute> FeedbackAttributes { get; set; }
        public string ProviderName { get; set; }
        public int LarsCode { get; set; }
        public string StandardUId { get; set; }
        public string StandardReference { get; set; }
        public bool ContactConsent { get; set; }
    }
}
