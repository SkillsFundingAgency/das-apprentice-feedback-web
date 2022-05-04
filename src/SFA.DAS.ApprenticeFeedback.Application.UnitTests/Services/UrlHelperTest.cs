using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Application.Settings;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeFeedback.Application.UnitTests.Services
{
    public class UrlHelperTest
    {
        [Test]
        [MoqInlineAutoData("http://FAT")]
        [MoqInlineAutoData("http://FAT/")]
        public void When_Building_FAT_Url_Review_Link_Then_Correct_URL_Is_Returned(string url, int larscode, long ukprn)
        {
            var appSettings = new AppSettings { FindApprenticeshipTrainingBaseUrl = url };
            var urlHelper = new UrlHelper(appSettings);

            var actual = urlHelper.FindApprenticeshipTrainingFeedbackUrl(ukprn, larscode);

            actual.Should().Be($"http://FAT/courses/{larscode}/providers/{ukprn}");
        }
    }
}
