using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Bindings
{
    [Binding]
    public class OuterApi
    {
        public static MockOuterApi Client { get; set; }

        private readonly TestContext _context;

        public OuterApi(TestContext context)
        {
            _context = context;
        }

        [BeforeScenario(Order = 1)]
        public void Initialise()
        {
            Client ??= new MockOuterApi();

            _context.OuterApi = Client;
        }

        [AfterScenario()]
        public void CleanUp()
        {
            Client?.Reset();
        }

        [AfterFeature()]
        public static void CleanUpFeature()
        {
            Client?.Dispose();
            Client = null;
        }
    }
}
