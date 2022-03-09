using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Application.Services
{
    public class ApprenticeFeedbackService : IApprenticeFeedbackService
    {
        private readonly IApprenticeFeedbackApi _apiClient;

        public ApprenticeFeedbackService(IApprenticeFeedbackApi apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task SubmitFeedback(PostSubmitFeedback request)
        {
            await _apiClient.SubmitFeedback(request);
        }

        public async Task<List<TrainingProvider>> GetTrainingProviders(Guid apprenticeId)
        {
            //var response = await _apiClient.GetTrainingProviders(apprenticeId);

            //var trainingProviders = response.TrainingProviders.Select(provider => new TrainingProvider
            //{
            //    Ukprn = provider.Ukprn,
            //    Name = provider.ProviderName,
            //    Apprenticeships = provider.Apprenticeships.Select(apprenticeship => (Apprenticeship)apprenticeship)
            //}).ToList();

            //return trainingProviders;

            // Singular new apprenticeship
            //return new List<TrainingProvider>
            //{
            //    new TrainingProvider
            //    {
            //        Ukprn = 100000001,
            //        Name = "Test provider 1",
            //        Apprenticeships = new List<Apprenticeship>
            //        {
            //            new Apprenticeship
            //            {
            //                LarsCode = 1,
            //                StartDate = DateTime.Now
            //            }
            //        }
            //    }
            //};

            return GetTrainingProviders();           

        }

        public async Task<TrainingProvider> GetTrainingProvider(Guid apprenticeId, long ukprn)
        {
            //var trainingProvider = await _apiClient.GetTrainingProvider(apprenticeId, ukprn);

            //return new TrainingProvider
            //{
            //    Ukprn = trainingProvider.Ukprn,
            //    Name = trainingProvider.ProviderName,
            //    Apprenticeships = trainingProvider.Apprenticeships.Select(apprenticeship => (Apprenticeship)apprenticeship)
            //};

            var trainingProviders = GetTrainingProviders();

            return trainingProviders.FirstOrDefault(p => p.Ukprn == ukprn);
        }

        private List<TrainingProvider> GetTrainingProviders()
        {
             return new List<TrainingProvider>
            {
                new TrainingProvider
                {
                    Ukprn = 100000003,
                    Name = "Test provider 3",
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
                    Ukprn = 100000001,
                    Name = "Test provider 2",
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
                    Name = "Test provider 1",
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
        }

        public async Task<List<FeedbackAttribute>> GetTrainingProviderAttributes()
        {
            var response = await _apiClient.GetProviderAttributes();

            return response.ProviderAttributes.Select(attribute => (FeedbackAttribute)attribute).ToList();
        }
    }
}
