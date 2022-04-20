using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
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

        public async Task<IEnumerable<Domain.Models.Feedback.TrainingProvider>> GetTrainingProviders(Guid apprenticeId)
        {
            // Get the training providers from the Outer Api
            //var response = await _apiClient.GetTrainingProviders(apprenticeId);

            var response = new GetTrainingProvidersResponse()
            {
                InitialDenyPeriodDays = 92,
                FinalAllowedPeriodDays = 92,
                RecentDenyPeriodDays = 14,
                TrainingProviders = new List<Domain.Api.Responses.TrainingProvider>() {
                    /*
                    new Domain.Api.Responses.TrainingProvider
                    {
                        UkPrn = 100000001,
                        ProviderName = "Test Provider Started Too Recently",
                        FeedbackEligibility = Domain.Api.Responses.FeedbackEligibility.Deny_TooSoon,
                        TimeWindow = new TimeSpan(days: 92, hours: 0, minutes: 0, seconds: 0),
                        SignificantDate = DateTime.UtcNow.AddDays(92)
                    },
                    */
                    
                    /*
                    new Domain.Api.Responses.TrainingProvider
                    {
                        UkPrn = 100000002,
                        ProviderName = "Test Provider Passed Too Long Ago",
                        FeedbackEligibility = Domain.Api.Responses.FeedbackEligibility.Deny_TooLateAfterPassing,
                        TimeWindow = new TimeSpan(days: 92, hours: 0, minutes: 0, seconds: 0),
                    },
                    */

                    /*
                    new Domain.Api.Responses.TrainingProvider
                    {
                        UkPrn = 100000003,
                        ProviderName = "Test Provider Withdrew Too Long Ago",
                        FeedbackEligibility = Domain.Api.Responses.FeedbackEligibility.Deny_TooLateAfterWithdrawing,
                        TimeWindow = new TimeSpan(days: 92, hours: 0, minutes: 0, seconds: 0),
                    },
                    */

                    /*
                    new Domain.Api.Responses.TrainingProvider
                    {
                        UkPrn = 100000004,
                        ProviderName = "Test Provider Has Given Feedback Recently",
                        FeedbackEligibility= Domain.Api.Responses.FeedbackEligibility.Deny_HasGivenFeedbackRecently,
                        DateSubmitted = DateTime.UtcNow.AddDays(-10),
                        SignificantDate = DateTime.UtcNow.AddDays(4)
                    },
                    */

                    /*
                    new Domain.Api.Responses.TrainingProvider
                    {
                        UkPrn = 100000005,
                        ProviderName = "Test Provider Has Given Final Feedback",
                        FeedbackEligibility = Domain.Api.Responses.FeedbackEligibility.Deny_HasGivenFinalFeedback,
                        TimeWindow = new TimeSpan(days: 92, hours: 0, minutes: 0, seconds: 0),
                    },
                    */

                    
                    new Domain.Api.Responses.TrainingProvider
                    {
                        UkPrn = 100000006,
                        ProviderName = "Test Provider Never Given Feedback",
                        FeedbackEligibility = Domain.Api.Responses.FeedbackEligibility.Allow,
                    },

                    /*
                    new Domain.Api.Responses.TrainingProvider
                    {
                        UkPrn = 100000007,
                        ProviderName = "Test Provider Long Enough Since Previous Feedback",
                        FeedbackEligibility = Domain.Api.Responses.FeedbackEligibility.Allow,
                        DateSubmitted = DateTime.UtcNow.AddMonths(-4),
                    }
                    */
                },
            };

            // automapper?
            return response.TrainingProviders.Select(tp => new Domain.Models.Feedback.TrainingProvider()
            {
                Name = tp.ProviderName,
                Ukprn = tp.UkPrn,
                DateSubmitted = tp.DateSubmitted,
                FeedbackEligibility = (Domain.Models.Feedback.FeedbackEligibility)Enum.Parse(typeof(Domain.Models.Feedback.FeedbackEligibility), tp.FeedbackEligibility.ToString()),
                TimeWindow = tp.TimeWindow,
                SignificantDate = tp.SignificantDate,
            });
        }

        public async Task<Domain.Models.Feedback.TrainingProvider> GetTrainingProvider(Guid apprenticeId, long ukprn)
        {
            // @ToDo:
            // Is there an api call for this on the outer api, or do we do what we are doing here?

            var providers = await GetTrainingProviders(apprenticeId);
            return providers.FirstOrDefault(p => p.Ukprn == ukprn);
        }

        public async Task<IEnumerable<FeedbackAttribute>> GetFeedbackAttributes()
        {
            /*
            var response = await _apiClient.GetProviderAttributes();
            return response.Select(attribute => (FeedbackAttribute)attribute).ToList();
            */
            return new List<FeedbackAttribute>() { new FeedbackAttribute() { Id = 1, Name = "Feedback attribute 1" } };
        }

        public Task SubmitFeedback(PostSubmitFeedback request)
        {
            //await _apiClient.SubmitFeedback(request);
            return Task.CompletedTask;
        }
    }
}
