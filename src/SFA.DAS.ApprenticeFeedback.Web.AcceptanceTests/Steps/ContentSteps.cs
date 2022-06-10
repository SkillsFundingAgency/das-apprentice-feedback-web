using FluentAssertions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Steps
{
    [Binding]
    public sealed class ContentSteps
    {
        private readonly TestContext _context;

        public ContentSteps(TestContext context)
        {
            _context = context;
        }

        [Then("the page content includes the following: (.*)")]
        public void ThenThePageContentIncludesTheFollowing(string expectedContent)
        {
            _context.TestActionResultContent.Should().Contain(expectedContent);
        }
    }
}
