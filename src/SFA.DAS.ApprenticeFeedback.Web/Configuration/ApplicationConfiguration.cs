using SFA.DAS.ApprenticeFeedback.Application.Settings;
using SFA.DAS.ApprenticePortal.SharedUi;
using SFA.DAS.ApprenticePortal.SharedUi.GoogleAnalytics;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.SharedUi.Zendesk;

namespace SFA.DAS.ApprenticeFeedback.Web.Configuration
{
    public class ApplicationConfiguration : ISharedUiConfiguration
    {
        public AuthenticationConfiguration Authentication { get; set; }
        public NavigationSectionUrls ApplicationUrls { get; set; }
        public OuterApiConfiguration ApprenticeFeedbackOuterApi { get; set; }
        public GoogleAnalyticsConfiguration GoogleAnalytics { get; set; }
        public ZenDeskConfiguration Zendesk { get; set; }
        public AppSettings AppSettings { get; set; }        
    }
}
