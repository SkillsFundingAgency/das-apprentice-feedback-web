using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Steps
{

    [Binding]
    public class IndexPageSteps
    {
        private readonly TestContext _context;
        private readonly RegisteredUserContext _userContext;

        public IndexPageSteps(TestContext context, RegisteredUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        [Given("the apprentice has logged in")]
        public void GivenTheApprenticeHasLoggedIn()
        {
            _context.Web.AuthoriseApprentice(_userContext.ApprenticeId);
        }

        [Given("the apprentice has multiple training providers")]
        public void GivenTheApprenticeHasMultipleTrainingProviders()
        {
            _context.OuterApi.MockServer.Given(
                Request.Create()
                    .UsingAnyMethod()
                    .WithPath($"/provider/{_userContext.ApprenticeId}")
                   )
                    .RespondWith(
                        Response.Create()
                            .WithStatusCode(200)
                            .WithBodyAsJson(new GetTrainingProvidersResponse
                            {
                                TrainingProviders = GetMultipleProviders()
                            }));
        }

        [When("accessing the index page")]
        public async Task WhenAccessingTheIndexPage()
        {
            var indexPageResponse = await _context.Web.Get("/");

            indexPageResponse.EnsureSuccessStatusCode();

            _context.TestActionResultContent = await indexPageResponse.Content.ReadAsStringAsync();
        }

        private List<TrainingProvider> GetSingleProvider()
        {
            return new List<TrainingProvider>
            {
                new TrainingProvider
                {
                    UkPrn = 100000003,
                    ProviderName = "Test provider 1",
                },
            };
        }
        private List<TrainingProvider> GetMultipleProviders()
        {
            return new List<TrainingProvider>
            {
                new TrainingProvider
                {
                    UkPrn = 100000003,
                    ProviderName = "Test provider 3",
                },
                new TrainingProvider
                {
                    UkPrn = 100000002,
                    ProviderName = "Test provider 2",
                },
                new TrainingProvider
                {
                    UkPrn = 100000001,
                    ProviderName = "Test provider 1",
                }
            };
        }
    }
}
