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
            try
            {
                // Get the training providers from the Outer Api
                var response = await _apiClient.GetTrainingProviders(apprenticeId);

                // automapper?
                return response.TrainingProviders.Select(tp => new Domain.Models.Feedback.TrainingProvider()
                {
                    Name = tp.ProviderName,
                    Ukprn = tp.UkPrn,
                    DateSubmitted = tp.LastFeedbackSubmittedDate,
                    FeedbackEligibility = (Domain.Models.Feedback.FeedbackEligibility)tp.FeedbackEligibility,
                    TimeWindow = tp.TimeWindow,
                    SignificantDate = tp.SignificantDate,
                });
            }
            catch(Exception ex)
            {
                throw;
            }
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
