using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Configuration;
using SFA.DAS.Testing.AutoFixture;
using System;

namespace SFA.DAS.ApprenticeFeedback.Application.UnitTests.Services
{
    public class UrlHelperTest
    {
        [Test]
        [MoqInlineAutoData("http://FAT")]
        [MoqInlineAutoData("http://FAT/")]
        public void When_Building_FAT_Url_Review_Link_Then_Correct_URL_Is_Returned(string url, int larscode, long ukprn)
        {
            var config = new FindApprenticeshipTrainingConfiguration { BaseUrl = url };

            var mockOptions = new Mock<IOptions<FindApprenticeshipTrainingConfiguration>>();
            mockOptions.Setup(c => c.Value).Returns(config);

            var urlHelper = new UrlHelper(mockOptions.Object);

            var actual = urlHelper.FindApprenticeshipTrainingFeedbackUrl(ukprn, larscode);

            actual.Should().Be($"http://FAT/courses/{larscode}/providers/{ukprn}");
        }
    }
}
