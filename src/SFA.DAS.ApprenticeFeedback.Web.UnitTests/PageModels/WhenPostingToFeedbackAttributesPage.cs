using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback;
using SFA.DAS.Testing.AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.PageModels
{
    public class WhenPostingToFeedbackAttributesPage
    {
        private FeedbackAttributesModel FeedbackAttributesPage;

        Mock<IApprenticeFeedbackSessionService> _mockSessionService;
        Mock<IApprenticeFeedbackService> _mockFeedbackService;

        [SetUp]
        public void Arrange()
        {
            _mockSessionService = new Mock<IApprenticeFeedbackSessionService>();
            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
            FeedbackAttributesPage = new FeedbackAttributesModel(_mockSessionService.Object, _mockFeedbackService.Object);
        }


        [Test, MoqAutoData]
        public PageResult And_ModelStateIsNotValid_AndOneOrLessErrors_ReturnPage(string key, string message)
        {

            FeedbackAttributesPage.ModelState.AddModelError(key, message);

            IActionResult result = FeedbackAttributesPage.OnPost();

            result.Should().BeOfType<PageResult>();
            return (PageResult)result;

        }

        [Test, MoqAutoData]
        public PageResult And_ModelStateIsNotValid_AndMoreThanOneError_AddCustomErrorMessageAndReturnPage
            (string key1, string message1, string key2, string message2)
        {

            FeedbackAttributesPage.ModelState.AddModelError(key1, message1);
            FeedbackAttributesPage.ModelState.AddModelError(key2, message2);

            var result = FeedbackAttributesPage.OnPost();

            FeedbackAttributesPage.ModelState.ErrorCount.Should().Be(3);
            result.Should().BeOfType<PageResult>();
            return (PageResult)result;

        }

        [Test]
        [MoqInlineAutoData(null)]
        [MoqInlineAutoData(false)]
        public RedirectToPageResult And_ModelStateIsValid_AndEditingHasNoValueOrIsFalse_RedirectToOverallRating(bool? editing, FeedbackRequest request)
        {

            FeedbackAttributesPage.Editing = editing;
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);

            IActionResult result = FeedbackAttributesPage.OnPost();

            FeedbackAttributesPage.ModelState.IsValid.Should().BeTrue();
            result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be("OverallRating");
            return (RedirectToPageResult)result;

        }

        [Test]
        [MoqInlineAutoData(true)]
        public RedirectToPageResult And_ModelStateIsValid_AndEditingIsTrue_RedirectToCheckYourAnswers(bool? editing, FeedbackRequest request)
        {
            FeedbackAttributesPage.Editing = editing;
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);

            IActionResult result = FeedbackAttributesPage.OnPost();

            FeedbackAttributesPage.ModelState.IsValid.Should().BeTrue();
            result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be("CheckYourAnswers");
            return(RedirectToPageResult)result;
        }

        [Test, MoqAutoData]
        public async Task And_FeedbackAttributesIsPopulated_FeedbackAttributesIsPopulatedCorrectly(List<FeedbackAttribute> feedbackAttributes, FeedbackRequest request)
        {
            _mockSessionService.Setup(s => s.GetFeedbackRequest()).Returns(request);
            feedbackAttributes = request.FeedbackAttributes;

            await FeedbackAttributesPage.OnGet(true);

            FeedbackAttributesPage.FeedbackAttributes.Should().BeEquivalentTo(feedbackAttributes);

        }

    }
}
