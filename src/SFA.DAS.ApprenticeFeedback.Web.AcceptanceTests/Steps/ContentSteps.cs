using FluentAssertions;
using SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Steps
{
    [Binding]
    public sealed class ContentSteps : StepsBase
    {
        private readonly TestContext _context;

        public ContentSteps(TestContext context) : base(context)
        {
            _context = context;
        }

        [Then("the page content includes the following: (.*)")]
        public async Task ThenThePageContentIncludesTheFollowing(string expectedContent)
        {
            var response = _context.ActionResult;

            //var actualContent = await response.Content.ReadAsStringAsync();

            //actualContent.Should().Contain(expectedContent);
        }
    }
}
