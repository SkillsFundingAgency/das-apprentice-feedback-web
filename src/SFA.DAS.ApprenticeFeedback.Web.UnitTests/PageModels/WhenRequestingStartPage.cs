using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
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
            StartPage = new StartModel(_mockSessionService.Object, _mockUrlHelper.Object, Mock.Of<ILogger<StartModel>>()); //setup the object so methods can be called to be tested
        }

        [Test]
        [MoqInlineAutoData(1,0)]
        [MoqInlineAutoData(0, 1)]
        public async Task And_NoUkrpnOrLarscodeIsProvidedInSession_RedirectsToIndex(int ukprn, int larscode, FeedbackRequest request)
        {
            request.Ukprn = ukprn; //arrange variables so that test is ready
            request.LarsCode = larscode;
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);

            var result = await StartPage.OnGet(); //call method to be tested

            result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be("./Index"); //assert what should happen
        }

        [Test, MoqAutoData]
        public async Task And_UkprnAndLarscodeProvidedInSession_FatLinkIsGeneratedCorrectly(FeedbackRequest feedbackRequest, string url)
        {
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(feedbackRequest);
            _mockUrlHelper.Setup(u => u.FATFeedback(feedbackRequest.Ukprn, feedbackRequest.LarsCode)).Returns(url);

            var result = await StartPage.OnGet();

            StartPage.FATLink.Should().Be(url);
        }
    }
}
