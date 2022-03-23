using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Configuration;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;
using SFA.DAS.Testing.AutoFixture;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.PageModels
{
    public class WhenRequestingStartPage
    {
        private StartModel StartPage;

        private Mock<IApprenticeFeedbackSessionService> _mockSessionService;
        private Mock<Domain.Interfaces.IUrlHelper> _mockUrlHelper;

        [SetUp]
        public void Arrange()
        {
            _mockSessionService = new Mock<IApprenticeFeedbackSessionService>();
            _mockUrlHelper = new Mock<Domain.Interfaces.IUrlHelper>();
            StartPage = new StartModel(_mockSessionService.Object, _mockUrlHelper.Object, Mock.Of<ILogger<StartModel>>());
        }

        [Test]
        [MoqInlineAutoData(1,0)]
        [MoqInlineAutoData(0, 1)]
        public async Task And_NoUkrpnOrLarscodeIsProvidedInSession_RedirectsToIndex(int ukprn, int larscode, FeedbackRequest request)
        {
            request.Ukprn = ukprn;
            request.LarsCode = larscode;
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);

            var result = await StartPage.OnGet();

            result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be("./Index");
        }

        [Test, MoqAutoData]
        public async Task And_UkprnAndLarscodeProvidedInSession_FatLinkIsGeneratedCorrectly(FeedbackRequest feedbackRequest, string url)
        {
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(feedbackRequest);
            _mockUrlHelper.Setup(u => u.FATFeedback(feedbackRequest.Ukprn, feedbackRequest.LarsCode));

            //testing provider name is populated
            var result = await StartPage.OnGet();

            var providerName = StartPage.ProviderName;

            providerName.Should().Be(feedbackRequest.TrainingProvider);

            //testing url
            var config = new FindApprenticeshipTrainingConfiguration { BaseUrl = url };

            var mockOptions = new Mock<IOptions<FindApprenticeshipTrainingConfiguration>>();
            mockOptions.Setup(c => c.Value).Returns(config);

            var urlHelper = new UrlHelper(mockOptions.Object);

            var actual = urlHelper.FATFeedback(feedbackRequest.Ukprn, feedbackRequest.LarsCode);

            actual.Should().Be($"{url}/courses/{feedbackRequest.LarsCode}/providers/{feedbackRequest.Ukprn}");
        }
    }

    // 3. Unit test will to mock IApprenticeFeedbackSessionService, UrlHelper   
    //       a. Happy Path -  New mock of Mock<IApprenticeFeedbackSessionService>  set up GetFeedbackRequest to return Ukprn and LarsCode
    //          Asserting FatLink is generated correctly with mocked get feedback request.
    //          Assert provider name is populated.
    //       b. Unhappy Path - New mock of Mock<IApprenticeFeedbackSessionService> To Return null for get feedback request
    //          Return a Redirect to the dashboard 
}
