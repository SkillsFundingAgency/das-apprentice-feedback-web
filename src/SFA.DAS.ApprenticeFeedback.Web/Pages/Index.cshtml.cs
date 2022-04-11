using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeFeedback.Domain.Extensions;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages
{
    [HideNavigationBar]
    public class IndexModel : PageModel, IHasBackLink
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;
        private readonly IApprenticeFeedbackSessionService _sessionService;

        public string DashboardLink { get; set; }
        public string Backlink => DashboardLink;

        // Textual descriptions of the feedback time periods.
        public string FeedbackRate { get; set; }
        public string FeedbackInitialDenyPeriod { get; set; }
        public string FeedbackFinalAllowPeriod { get; set; }

        // provider viewmodel
        public class ProviderItem
        {
            public const string NO_SUBMITTED_DATE = "N/A";

            public long Ukprn { get; set; }
            public string Name { get; set; }
            public string DateSubmitted { get; set; }
            public FeedbackEligibility FeedbackEligibility { get; set; }
            public TimeSpan? TimeWindow { get; set; }
            public DateTime? SignificantDate { get; set; }
            public bool Show { get; set; }
        }

        public List<ProviderItem> TrainingProviderItems { get; set; }


        public IndexModel(ILogger<IndexModel> logger
            , IApprenticeFeedbackService apprenticeFeedbackService
            , NavigationUrlHelper urlHelper
            , IApprenticeFeedbackSessionService sessionService
        )
        {
            _logger = logger;
            _apprenticeFeedbackService = apprenticeFeedbackService;
            _sessionService = sessionService;
            DashboardLink = urlHelper.Generate(NavigationSection.Home);
        }


        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user)
        {
            var providers = await _apprenticeFeedbackService.GetTrainingProviders(user.ApprenticeId);
            var allItems = GenerateProviderItems(providers);
            TrainingProviderItems = allItems.Where(tpi => tpi.Show == true).ToList();

            if (TrainingProviderItems.Count == 1)
            {
                var feedbackContext = new FeedbackContext();
                feedbackContext.FeedbackEligibility = TrainingProviderItems[0].FeedbackEligibility;
                feedbackContext.TimeWindow = TrainingProviderItems[0].TimeWindow;
                feedbackContext.SignificantDate = TrainingProviderItems[0].SignificantDate;
                _sessionService.SetFeedbackContext(feedbackContext);

                if(feedbackContext.FeedbackEligibility == FeedbackEligibility.Allow) {
                    return Redirect($"/start/{TrainingProviderItems[0].Ukprn}");
                }
                return Redirect("/status");
            }

            FeedbackRate = _apprenticeFeedbackService.RecentDenyPeriod.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month);
            FeedbackInitialDenyPeriod = _apprenticeFeedbackService.InitialDenyPeriod.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month);
            FeedbackFinalAllowPeriod = _apprenticeFeedbackService.FinalAllowPeriod.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month);
            return Page();                
        }


        private List<ProviderItem> GenerateProviderItems(IEnumerable<TrainingProvider> providers)
        {
            var vm = new List<ProviderItem>();

            foreach(var provider in providers)
            {
                var mostRecentlyStartedApprenticeship = provider?.GetMostRecentlyStartedApprenticeship();
                var feedbackEligiblity = _apprenticeFeedbackService.GetFeedbackEligibility(mostRecentlyStartedApprenticeship);

                var item = new ProviderItem()
                {
                    Ukprn = provider.Ukprn,
                    Name = provider.Name,
                    DateSubmitted = ProviderItem.NO_SUBMITTED_DATE,
                    FeedbackEligibility = feedbackEligiblity.feedbackEligibility,
                    TimeWindow = feedbackEligiblity.timeWindow,
                    SignificantDate = feedbackEligiblity.significantDate,
                    Show = true,
                };

                if(null != mostRecentlyStartedApprenticeship.FeedbackCompletionDates 
                    && !mostRecentlyStartedApprenticeship.FeedbackCompletionDates.ContainsCountItems(0))
                {
                    item.DateSubmitted = mostRecentlyStartedApprenticeship.FeedbackCompletionDates.First().ToString("d MMM yyyy");
                }

                if(item.FeedbackEligibility == FeedbackEligibility.Deny_TooLateAfterPassing 
                    || item.FeedbackEligibility == FeedbackEligibility.Deny_TooLateAfterWithdrawing)
                {
                    // @ToDo: are other possibilities other than passed or withdrew that we want to exclude from the UI?
                    item.Show = false;
                }

                vm.Add(item);
            }

            return vm;
        }
    }
}
