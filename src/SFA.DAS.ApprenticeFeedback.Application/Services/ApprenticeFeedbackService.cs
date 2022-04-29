﻿using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
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

        public TimeSpan InitialDenyPeriod { get; internal set; }

        public TimeSpan RecentDenyPeriod { get; internal set; }

        public TimeSpan FinalAllowPeriod { get; internal set; }

        public int MinimumActiveApprenticeshipCount { get; internal set; }

        public ApprenticeFeedbackService(IApprenticeFeedbackApi apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Domain.Models.Feedback.TrainingProvider>> GetTrainingProviders(Guid apprenticeId)
        {
            // Get the training provider data set from the Outer Api
            var response = await _apiClient.GetTrainingProviders(apprenticeId);

            // Initialise the config parameters
            InitialDenyPeriod = new TimeSpan(days: response.InitialDenyPeriodDays, 0, 0, 0);
            RecentDenyPeriod = new TimeSpan(days: response.RecentDenyPeriodDays, 0, 0, 0);
            FinalAllowPeriod = new TimeSpan(days: response.FinalAllowedPeriodDays, 0, 0, 0);
            MinimumActiveApprenticeshipCount = response.MinimumActiveApprenticeshipCount;

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

        public async Task<Domain.Models.Feedback.TrainingProvider> GetTrainingProvider(Guid apprenticeId, long ukprn)
        {
            // @ToDo:
            // Is there an api call for this on the outer api, or do we do what we are doing here?

            var providers = await GetTrainingProviders(apprenticeId);
            return providers.FirstOrDefault(p => p.Ukprn == ukprn);
        }

        public async Task<IEnumerable<FeedbackAttribute>> GetFeedbackAttributes()
        {
            var response = await _apiClient.GetFeedbackAttributes();
            return response.Select(attribute => (FeedbackAttribute)attribute).ToList();
        }

        public async Task SubmitFeedback(PostSubmitFeedback request)
        {
            await _apiClient.SubmitFeedback(request);
        }
    }
}
