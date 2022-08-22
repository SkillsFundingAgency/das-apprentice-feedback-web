using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class GetApprenticeFeedbackResponse
    {
        [JsonPropertyName("feedbackTargets")]
        public IEnumerable<FeedbackTarget> FeedbackTargets { get; set; }
    }

    public class FeedbackTarget
    {
        [JsonPropertyName("apprenticeId")]
        public Guid ApprenticeId { get; set; }
        [JsonPropertyName("apprenticeshipId")]
        public long ApprenticeshipId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
        [JsonPropertyName("results")]
        public IEnumerable<FeedbackResult> Results { get; set; }
    }

    public class FeedbackResult
    {
        [JsonPropertyName("ukprn")]
        public long Ukprn { get; set; }
        [JsonPropertyName("larsCode")]
        public int LarsCode { get; set; }
        [JsonPropertyName("providerName")]
        public string ProviderName { get; set; }
        [JsonPropertyName("dateTimeCompleted")]
        public DateTime DateTimeCompleted { get; set; }
    }
}
