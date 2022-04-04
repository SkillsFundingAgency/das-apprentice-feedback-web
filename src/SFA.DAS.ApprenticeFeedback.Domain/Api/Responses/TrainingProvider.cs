using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class TrainingProvider
    {
        [JsonProperty("ukprn")]
        public long Ukprn { get; set; }
        [JsonProperty("providerName")]
        public string ProviderName { get; set; }
        [JsonProperty("apprenticeships")]
        public IEnumerable<Apprenticeship> Apprenticeships { get; set; }
    }
}
