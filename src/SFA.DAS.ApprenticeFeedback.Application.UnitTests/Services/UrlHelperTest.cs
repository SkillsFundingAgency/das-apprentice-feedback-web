using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Application.Settings;

namespace SFA.DAS.ApprenticeFeedback.Application.UnitTests.Services
{
    public class UrlHelperTest
    {
        [Test]
        public void When_Building_FAT_Url_Review_Link_Then_Correct_URL_Is_Returned()
        {
            var url = "http://FAT";
            int larscode = 232;
            long ukprn = 15000034;
            var appSettings = new AppSettings { FindApprenticeshipTrainingBaseUrl = url };
            var urlHelper = new UrlHelper(appSettings);

            var actual = urlHelper.FindApprenticeshipTrainingFeedbackUrl(ukprn, larscode);

            actual.Should().Be($"http://FAT/courses/{larscode}/providers/{ukprn}");
        }
    }
}
