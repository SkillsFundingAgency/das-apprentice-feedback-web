using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Steps
{

    [Binding]
    public class IndexPageSteps : StepsBase
    {
        private readonly TestContext _context;
        private readonly RegisteredUserContext _userContext;

        public IndexPageSteps(TestContext context, RegisteredUserContext userContext) : base(context)
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
                    //.WithPath($"/apprentices/*/apprenticeships/{_userContext.ApprenticeId}"))
                   // .WithPath($"/"))
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
            var indexPageResponse = await _context.Web.Get("error");

            //_context.
        }

        private List<TrainingProvider> GetMultipleProviders()
        {
            /*
            return new List<TrainingProvider>
            {
                new TrainingProvider
                {
                    Ukprn = 100000003,
                    ProviderName = "Test provider 3",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 3,
                            StartDate = DateTime.Now.AddMonths(-4),
                            FeedbackCompletionDates = new List<DateTime>()
                        }
                    }
                },
                new TrainingProvider
                {
                    Ukprn = 100000002,
                    ProviderName = "Test provider 2",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 1,
                            StartDate = DateTime.Now,
                            FeedbackCompletionDates = new List<DateTime>()
                        }
                    }
                },
                new TrainingProvider
                {
                    Ukprn = 100000001,
                    ProviderName = "Test provider 1",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 1,
                            StartDate = DateTime.Now.AddMonths(-12),
                            FeedbackCompletionDates = new List<DateTime>
                            {
                                DateTime.Now.AddDays(-5)
                            }
                        }
                    }
                }
            };
            */
            return null;
        }
    }
}
