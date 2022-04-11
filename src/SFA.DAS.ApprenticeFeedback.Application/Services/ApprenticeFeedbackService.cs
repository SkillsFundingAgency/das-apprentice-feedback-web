using SFA.DAS.ApprenticeFeedback.Application.Settings;
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
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly TimeSpan _initialDenyPeriod;
        private readonly TimeSpan _finalAllowPeriod;
        private readonly TimeSpan _recentDenyPeriod;

        public TimeSpan InitialDenyPeriod { get { return _initialDenyPeriod; } }
        public TimeSpan RecentDenyPeriod { get { return _recentDenyPeriod; } }
        public TimeSpan FinalAllowPeriod { get { return _finalAllowPeriod; } }

        public ApprenticeFeedbackService(IApprenticeFeedbackApi apiClient, IDateTimeProvider dateTimeProvider, FeedbackSettings settings)
        {
            _apiClient = apiClient;
            _dateTimeProvider = dateTimeProvider;
            _initialDenyPeriod = new TimeSpan(days: settings.InitialDenyPeriodDays, hours: 0, minutes: 0, seconds: 0);
            _finalAllowPeriod = new TimeSpan(days: settings.FinalAllowPeriodDays, hours: 0, minutes: 0, seconds: 0);
            _recentDenyPeriod = new TimeSpan(days: settings.RecentDenyPeriodDays, hours: 0, minutes: 0, seconds: 0);
        }

        public async Task<IEnumerable<TrainingProvider>> GetTrainingProviders(Guid apprenticeId)
        {
            // @ToDo:

            // Get the training providers from the Outer Api
            //var response = await _apiClient.GetTrainingProviders(apprenticeId);
            // map response.TrainingProviders to our model.TrainingProviders

            // Temporary hard-coded test data
            return new List<TrainingProvider>
            {
                new TrainingProvider
                {
                    Ukprn = 100000001,
                    Name = "Test Provider Started Too Recently",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 1,
                            StartDate = DateTime.Now.AddMonths(-1),
                        }
                    }
                },

                new TrainingProvider
                {
                    Ukprn = 100000002,
                    Name = "Test Provider Passed Too Long Ago",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 2,
                            StartDate = DateTime.Now.AddMonths(-6),
                            EndDate = DateTime.Now.AddMonths(-4),
                            Status = ApprenticeshipStatus.Pass
                        }
                    }
                },

                new TrainingProvider
                {
                    Ukprn = 100000003,
                    Name = "Test Provider Withdrew Too Long Ago",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 3,
                            StartDate = DateTime.Now.AddMonths(-6),
                            EndDate = DateTime.Now.AddMonths(-4),
                            Status = ApprenticeshipStatus.Withdraw
                        }
                    }
                },

                new TrainingProvider
                {
                    Ukprn = 100000004,
                    Name = "Test Provider Has Given Feedback Recently",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 4,
                            StartDate = DateTime.Now.AddMonths(-12),
                            FeedbackCompletionDates = new List<DateTime>() { DateTime.Now.AddDays(-5) }
                        }
                    }
                },

                new TrainingProvider
                {
                    Ukprn = 100000005,
                    Name = "Test Provider Has Given Final Feedback",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 5,
                            StartDate = DateTime.Now.AddMonths(-12),
                            FeedbackCompletionDates = new List<DateTime>() { DateTime.Now.AddDays(-5) },
                            EndDate = DateTime.Now.AddMonths(-1),
                        }
                    }
                },

                new TrainingProvider
                {
                    Ukprn = 100000006,
                    Name = "Test Provider Never Given Feedback",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 6,
                            StartDate = DateTime.Now.AddMonths(-12),
                        }
                    }
                },

                new TrainingProvider
                {
                    Ukprn = 100000007,
                    Name = "Test Provider Long Enough Since Previous Feedback",
                    Apprenticeships = new List<Apprenticeship>
                    {
                        new Apprenticeship
                        {
                            LarsCode = 7,
                            StartDate = DateTime.Now.AddMonths(-12),
                            FeedbackCompletionDates = new List<DateTime>() { DateTime.Now.AddMonths(-5) },
                        }
                    }
                }

            }.AsEnumerable();
        }

        public async Task<TrainingProvider> GetTrainingProvider(Guid apprenticeId, long ukprn)
        {
            // @ToDo:
            // Is there an api call for this on the outer api, or do we do what we are doing here?

            var providers = await GetTrainingProviders(apprenticeId);
            return providers.FirstOrDefault(p => p.Ukprn == ukprn);
        }

        public async Task<IEnumerable<FeedbackAttribute>> GetFeedbackAttributes()
        {
            var response = await _apiClient.GetProviderAttributes();

            return response.Select(attribute => (FeedbackAttribute)attribute).ToList();
        }
        
        public (FeedbackEligibility feedbackEligibility, DateTime? significantDate, TimeSpan? timeWindow) GetFeedbackEligibility(Apprenticeship apprenticeship)
        {
            if (null == apprenticeship) throw new ArgumentNullException(nameof(apprenticeship));

            (FeedbackEligibility feedbackEligibility, DateTime? significantDate, TimeSpan? timeWindow) result = (FeedbackEligibility.Allow, null, null);

            var tooSoonCheck = IsTooSoonForFeedback(apprenticeship);
            if (tooSoonCheck.feedbackFlag)
            {
                result.feedbackEligibility = FeedbackEligibility.Deny_TooSoon;
                result.timeWindow = tooSoonCheck.timeWindow;
                result.significantDate = tooSoonCheck.significantDate;
            }

            if(FeedbackEligibility.Allow == result.feedbackEligibility && HasPassed(apprenticeship) )
            {
                var tooLateAfterPassingCheck = IsTooLateAfterEndDate(apprenticeship);
                if (tooLateAfterPassingCheck.feedbackFlag)
                {
                    result.feedbackEligibility = FeedbackEligibility.Deny_TooLateAfterPassing;
                    result.timeWindow = tooLateAfterPassingCheck.timeWindow;
                    result.significantDate = tooLateAfterPassingCheck.significantDate;
                }
            }

            if (FeedbackEligibility.Allow == result.feedbackEligibility && HasWithdrawn(apprenticeship))
            {
                var tooLateAfterWithdrawingCheck = IsTooLateAfterEndDate(apprenticeship);
                if (tooLateAfterWithdrawingCheck.feedbackFlag)
                {
                    result.feedbackEligibility = FeedbackEligibility.Deny_TooLateAfterWithdrawing;
                    result.timeWindow = tooLateAfterWithdrawingCheck.timeWindow;
                    result.significantDate = tooLateAfterWithdrawingCheck.significantDate;
                }
            }

            if (FeedbackEligibility.Allow == result.feedbackEligibility)
            {
                var recentFeedbackCheck = HasGivenFeedbackRecently(apprenticeship);
                if (recentFeedbackCheck.feedbackFlag)
                {
                    if(apprenticeship.EndDate.HasValue)
                    {
                        result.feedbackEligibility = FeedbackEligibility.Deny_HasGivenFinalFeedback;  
                    }
                    else
                    {
                        result.feedbackEligibility = FeedbackEligibility.Deny_HasGivenFeedbackRecently;
                    }
                    result.timeWindow = recentFeedbackCheck.timeWindow;
                    result.significantDate = recentFeedbackCheck.significantDate;
                }
            }

            return result;
        }

        private (bool feedbackFlag, DateTime? significantDate, TimeSpan? timeWindow) IsTooSoonForFeedback(Apprenticeship apprenticeship)
        {
            var significantDate = apprenticeship.StartDate.Date + _initialDenyPeriod;
            var timeWindow = _initialDenyPeriod;
            if (significantDate.Date > _dateTimeProvider.UtcNow.Date)
                return (true, significantDate, timeWindow);
            return (false, null, null);
        }

        private (bool feedbackFlag, DateTime? significantDate, TimeSpan? timeWindow) IsTooLateAfterEndDate(Apprenticeship apprenticeship)
        {
            if (!apprenticeship.EndDate.HasValue) return (false, null, null);
            var significantDate = apprenticeship.EndDate.Value.Date + _finalAllowPeriod;
            var timeWindow = _finalAllowPeriod;
            if (significantDate < _dateTimeProvider.UtcNow.Date)
                return (true, significantDate, timeWindow);
            return (false, null, null);
        }

        private bool HasPassed(Apprenticeship apprenticeship)
        {
            if (!apprenticeship.EndDate.HasValue) return false;
            if (string.IsNullOrWhiteSpace(apprenticeship.Status)) return false;
            return apprenticeship.Status.Equals(ApprenticeshipStatus.Pass, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool HasWithdrawn(Apprenticeship apprenticeship)
        {
            if (!apprenticeship.EndDate.HasValue) return false;
            if (string.IsNullOrWhiteSpace(apprenticeship.Status)) return false;
            return apprenticeship.Status.Equals(ApprenticeshipStatus.Withdraw, StringComparison.InvariantCultureIgnoreCase);
        }

        private (bool feedbackFlag,DateTime? significantDate, TimeSpan? timeWindow) HasGivenFeedbackRecently(Apprenticeship apprenticeship)
        {
            var mostRecentFeedbackDate = apprenticeship.GetMostRecentFeedbackCompletionDate();
            if (null == mostRecentFeedbackDate || !mostRecentFeedbackDate.HasValue) return (false,null, null);
            var significantDate = mostRecentFeedbackDate.Value.Date + _recentDenyPeriod;
            var timeWindow = _recentDenyPeriod;
            if(significantDate > _dateTimeProvider.UtcNow.Date)
                return (true, significantDate, timeWindow);
            return (false, null, null);
        }
    }
}
