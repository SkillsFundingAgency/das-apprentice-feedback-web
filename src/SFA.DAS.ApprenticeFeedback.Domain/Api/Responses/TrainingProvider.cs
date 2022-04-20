using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public enum FeedbackEligibility
    {
        Allow,
        Deny_TooSoon,
        Deny_TooLateAfterPassing,
        Deny_TooLateAfterWithdrawing,
        Deny_HasGivenFeedbackRecently,
        Deny_HasGivenFinalFeedback
    }


    public class TrainingProvider
    {
        /*
        [JsonProperty("ukprn")]
        public long Ukprn { get; set; }
        [JsonProperty("providerName")]
        public string ProviderName { get; set; }
        [JsonProperty("apprenticeships")]
        public IEnumerable<Apprenticeship> Apprenticeships { get; set; }
        */


        public string ProviderName { get; set; }
        public long UkPrn { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public FeedbackEligibility FeedbackEligibility { get; set; } 
        public TimeSpan? TimeWindow { get; set; }
        public DateTime? SignificantDate { get; set; }


    }
}
