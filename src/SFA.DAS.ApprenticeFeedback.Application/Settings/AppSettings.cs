using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Application.Settings
{
    public class AppSettings
    {
        public List<EngagementLink> EngagementLinks { get; set; }
        public string FindApprenticeshipTrainingBaseUrl { get; set; }
        public string NServiceBusConnectionString { get; set; }
        public string NServiceBusLicense { get; set; }
    }
}
