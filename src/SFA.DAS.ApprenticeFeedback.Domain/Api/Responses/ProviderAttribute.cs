using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class ProviderAttribute
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
