using SFA.DAS.ApprenticeFeedback.Application.Settings;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;

namespace SFA.DAS.ApprenticeFeedback.Application.Services
{
    public class UrlHelper : IUrlHelper
    {
        private readonly AppSettings _appSettings;

        public UrlHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string FindApprenticeshipTrainingFeedbackUrl(long ukprn, int larscode)
        {
            var url = _appSettings.FindApprenticeshipTrainingBaseUrl;

            if (!url.EndsWith("/"))
            {
                url += "/";
            }

            return url + $"courses/{larscode}/providers/{ukprn}";
        }
    }
}
