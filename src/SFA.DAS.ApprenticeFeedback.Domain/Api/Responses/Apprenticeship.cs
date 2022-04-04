using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class Apprenticeship
    {
        [JsonProperty("larsCode")]
        public int LarsCode { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; } // completed, paused, stopped
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("feedbackCompletionDates")]
        public IEnumerable<DateTime> FeedbackCompletionDates { get; set; }
    }
}
