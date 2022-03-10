using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class GetProviderAttributesResponse
    {
        [JsonProperty("providerAttributes")]
        public IEnumerable<ProviderAttribute> ProviderAttributes { get; set; }
    }
}
