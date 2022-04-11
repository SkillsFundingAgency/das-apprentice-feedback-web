using Microsoft.Extensions.Options;
using SFA.DAS.ApprenticeFeedback.Domain.Configuration;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;

namespace SFA.DAS.ApprenticeFeedback.Application.Services
{
    public class UrlHelper : IUrlHelper
    {
        private readonly FindApprenticeshipTrainingConfiguration _findApprenticeshipTrainingConfiguration;

        public UrlHelper(IOptions<FindApprenticeshipTrainingConfiguration> options)
        {
            _findApprenticeshipTrainingConfiguration = options.Value;
        }

        public string FindApprenticeshipTrainingFeedbackUrl(long ukprn, int larscode)
        {
            var url = _findApprenticeshipTrainingConfiguration.BaseUrl;

            if (!url.EndsWith("/"))
            {
                url += "/";
            }

            return url + $"courses/{larscode}/providers/{ukprn}";
        }
    }
}
