//using Microsoft.Extensions.Logging;
//using Moq;
//using NUnit.Framework;
//using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
//using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
//using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
//using SFA.DAS.ApprenticeFeedback.Web.Pages;
//using SFA.DAS.ApprenticePortal.SharedUi.Menu;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Pages
//{
//    public class WhenRequestingIndexPage
//    {
//        private IndexModel IndexPage;

//        private Mock<IApprenticeFeedbackService> _mockFeedbackService;
//        private Mock<IApprenticeFeedbackSessionService> _mockSession;
//        private NavigationUrlHelper _urlHelper;

//        [SetUp]
//        public void Arrange()
//        {
//            _mockFeedbackService = new Mock<IApprenticeFeedbackService>();
//            _mockSession = new Mock<IApprenticeFeedbackSessionService>();

//            var urls = new NavigationSectionUrls();
//            urls.ApprenticeHomeUrl = new Uri("https://localhost:5001/");

//            _urlHelper = new NavigationUrlHelper(urls);

//            IndexPage = new IndexModel(_mockFeedbackService.Object, _mockSession.Object, _urlHelper, Mock.Of<ILogger<IndexModel>>());
//        }

//        public async Task And_MultipleTrainingProvidersAreReturned_Then_ShowIndexPage()
//        {

//        }

//        public async Task And_SingleProviderReturned_Then_RedirectToStartPage()
//        {
//            var response = new List<TrainingProvider>
//            {
//                GetTrainingProviderValidForFeedback()
//            };

//            var result = IndexPage.OnGet() 
//        }

//        private TrainingProvider GetValidTrainingProvider()
//        {

//        }

//        private List<TrainingProvider> GetMultipleTrainingProviders()
//        {
//            return new List<TrainingProvider>
//            {
//                GetTrainingProviderValidForFeedback(),
//                new TrainingProvider
//                {
//                    Ukprn = 100000001,
//                    Name = "Test provider 2",
//                    Apprenticeships = new List<Apprenticeship>
//                    {
//                        new Apprenticeship
//                        {
//                            LarsCode = 1,
//                            StartDate = DateTime.Now,
//                            FeedbackCompletionDates = new List<DateTime>()
//                        }
//                    }
//                },
//                new TrainingProvider
//                {
//                    Ukprn = 100000001,
//                    Name = "Test provider 1",
//                    Apprenticeships = new List<Apprenticeship>
//                    {
//                        new Apprenticeship
//                        {
//                            LarsCode = 1,
//                            StartDate = DateTime.Now.AddMonths(-12),
//                            FeedbackCompletionDates = new List<DateTime>
//                            {
//                                DateTime.Now.AddDays(-5)
//                            }
//                        }
//                    }
//                }
//            };
//        }

//        private TrainingProvider GetTrainingProviderValidForFeedback()
//        {
//            return new TrainingProvider
//            {
//                Ukprn = 100000003,
//                Name = "Test provider 3",
//                Apprenticeships = new List<Apprenticeship>
//                    {
//                        new Apprenticeship
//                        {
//                            LarsCode = 3,
//                            StartDate = DateTime.Now.AddMonths(-4),
//                            FeedbackCompletionDates = new List<DateTime>()
//                        }
//                    }
//            };
//        }
//    }
//}
