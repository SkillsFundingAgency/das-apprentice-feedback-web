﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public class GetTrainingProvidersResponse
    {
		public int RecentDenyPeriodDays { get; set; }
		public int InitialDenyPeriodDays { get; set; }
		public int FinalAllowedPeriodDays { get; set; }

		public IEnumerable<TrainingProvider> TrainingProviders { get; set; }

        //[JsonProperty("trainingProviders")]
        //public IEnumerable<TrainingProvider> TrainingProviders { get; set; }
    }
}
