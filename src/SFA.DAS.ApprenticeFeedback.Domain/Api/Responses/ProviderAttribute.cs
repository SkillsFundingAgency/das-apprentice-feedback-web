using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class ProviderAttribute
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
