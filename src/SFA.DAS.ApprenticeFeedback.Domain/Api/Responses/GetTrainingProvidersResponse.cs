using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class GetTrainingProvidersResponse
    {
        [JsonProperty("trainingProviders")]
        public IEnumerable<TrainingProvider> TrainingProviders { get; set; }
    }
}
