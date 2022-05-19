using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class TrainingProviderResponse
    {
        public int RecentDenyPeriodDays { get; set; }
        public int InitialDenyPeriodDays { get; set; }
        public int FinalAllowedPeriodDays { get; set; }
        public int MinimumActiveApprenticeshipCount { get; set; }
        public IEnumerable<TrainingProvider> TrainingProviders { get; set; }
    }
}
