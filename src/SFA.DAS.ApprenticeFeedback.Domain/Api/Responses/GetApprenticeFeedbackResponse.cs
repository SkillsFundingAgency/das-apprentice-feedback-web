using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class GetApprenticeFeedbackResponse
    {
        [JsonProperty("feedbackTargets")]
        public IEnumerable<FeedbackTarget> FeedbackTargets { get; set; }
    }

    public class FeedbackTarget
    {
        [JsonProperty("apprenticeId")]
        public Guid ApprenticeId { get; set; }
        [JsonProperty("apprenticeshipId")]
        public long ApprenticeshipId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }
        [JsonProperty("results")]
        public IEnumerable<FeedbackResult> Results { get; set; }
    }

    public class FeedbackResult
    {
        [JsonProperty("ukprn")]
        public long Ukprn { get; set; }
        [JsonProperty("larsCode")]
        public int LarsCode { get; set; }
        [JsonProperty("providerName")]
        public string ProviderName { get; set; }
        [JsonProperty("dateTimeCompleted")]
        public DateTime DateTimeCompleted { get; set; }
    }
}
