//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Moq;
//using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
//using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
//using SFA.DAS.ApprenticeFeedback.Web.Startup;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using TechTalk.SpecFlow;

//namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Infrastructure
//{
//    [Binding]
//    public class TestEnvironmentManagement
//    {
//        private readonly ScenarioContext _context;
//        private static HttpClient _staticClient;
//        //private static IWireMockServer _staticApiServer;

//        private Mock<IApprenticeFeedbackService> _mockFeedbackService;

//        private static TestServer _server;

//        private LocalWebApplicationFactory<ApplicationStartup> _webApp;

//        public TestEnvironmentManagement(ScenarioContext context)
//        {
//            _context = context;
//        }

//        [BeforeScenario]
//        public void StartWebAppWithMockApiClient()
//        {
//            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
//            _mockFeedbackService.Setup(x => x.GetTrainingProviders(It.IsAny<Guid>()))
//                .ReturnsAsync(new List<TrainingProvider>());

//            _server = new TestServer(new WebHostBuilder()
//                .ConfigureTestServices(services => ConfigureTestServices(services, _mockFeedbackService))
//                .UseEnvironment(Environments.Development)
//                .UseStartup<ApplicationStartup>()
//                .UseConfiguration(ConfigBuilder.GenerateConfiguration()));

//            _staticClient = _server.CreateClient();

//            _context.Set(_mockFeedbackService, ContextKeys.MockApiClient);
//            _context.Set(_staticClient, ContextKeys.HttpClient);
//        }

//        private void ConfigureTestServices(IServiceCollection serviceCollection, Mock<IApprenticeFeedbackService> mockFeedbackService)
//        {
//            foreach (var descriptor in serviceCollection.Where(
//                d => d.ServiceType ==
//                     typeof(IApprenticeFeedbackService)).ToList())
//            {
//                serviceCollection.Remove(descriptor);
//            }

//            serviceCollection.AddSingleton(mockFeedbackService);
//            serviceCollection.AddSingleton(mockFeedbackService.Object);
//        }
//    }
//}
