using System.Text.Json.Serialization;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class FeedbackAttribute
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("ordering")]
        public int Ordering { get; set; }
    }
}
