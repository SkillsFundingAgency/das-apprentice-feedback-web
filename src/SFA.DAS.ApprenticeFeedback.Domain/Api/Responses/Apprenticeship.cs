using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class Apprenticeship
    {
        [JsonPropertyName("larsCode")]
        public int LarsCode { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; } // completed, paused, stopped
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("feedbackCompletionDates")]
        public IEnumerable<DateTime> FeedbackCompletionDates { get; set; }
    }
}
