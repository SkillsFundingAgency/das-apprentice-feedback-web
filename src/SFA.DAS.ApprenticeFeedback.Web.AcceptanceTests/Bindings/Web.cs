using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing.Handlers;
using SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Hooks;
using SFA.DAS.ApprenticeFeedback.Web.Startup;
using SFA.DAS.ApprenticePortal.Authentication.TestHelpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Bindings
{
    [Binding]
    public class Web
    {
        public static HttpClient Client { get; set; }
        public static Dictionary<string, string> Config { get; set; }
        public static LocalWebApplicationFactory<ApplicationStartup> Factory { get; set; }
        public static CookieContainer Cookies { get; set; }

        public static Hook<IActionResult> ActionResultHook;

        private readonly TestContext _context;

        public Web(TestContext context)
        {
            _context = context;
        }

        [BeforeScenario()]
        public void Initialise()
        {
            if (Client == null)
            {
                Config = new Dictionary<string, string>
                {
                    {"EnvironmentName", "ACCEPTANCE_TESTS"},
                    {"Authentication:MetadataAddress", _context.IdentityServiceUrl},
                    {"ApprenticeFeedbackOuterApi:ApiBaseUrl", _context.OuterApi?.BaseAddress ?? "https://api/"},
                    {"ApplicationUrls:ApprenticeHomeUrl", "https://home/"},
                    {"ApplicationUrls:ApprenticeAccountsUrl", "https://account/"},
                    {"ApplicationUrls:ApprenticeCommitmentsUrl", "http://commitments/"},
                    {"ApplicationUrls:ApprenticeLoginUrl", "https://login/"},
                    {"ApplicationUrls:ApprenticeFeedbackUrl", "https://localhost"}
                };

                ActionResultHook = new Hook<IActionResult>();

                Factory = new LocalWebApplicationFactory<ApplicationStartup>(Config, ActionResultHook);
                var handler = new CookieContainerHandler()
                {
                    InnerHandler = Factory.Server.CreateHandler(),
                };

                Client = new HttpClient(handler) { BaseAddress = Factory.Server.BaseAddress };
                Cookies = handler.Container;
            }

            _context.Web = new ApprenticeFeedbackWeb(Client, ActionResultHook, Config);

            AuthenticationHandlerForTesting.Authentications.Clear();
        }

        [AfterScenario()]
        public void CleanUpScenario()
        {
            _context?.Web?.Dispose();
        }

        [AfterFeature()]
        public static void CleanUpFeature()
        {
            Client?.Dispose();
            Factory?.Dispose();
            Client = null;
        }
    }
}
