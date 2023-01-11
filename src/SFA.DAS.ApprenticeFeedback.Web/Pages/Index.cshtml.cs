using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeFeedback.Domain.Extensions;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;
        private readonly IApprenticeFeedbackSessionService _sessionService;

        // Textual descriptions of the feedback time periods.
        public string FeedbackRate { get; set; }
        public string FeedbackInitialDenyPeriod { get; set; }
        public string FeedbackFinalAllowPeriod { get; set; }

        public IEnumerable<TrainingProvider> TrainingProviderItems { get; set; }
        public IndexModel(ILogger<IndexModel> logger
            , IApprenticeFeedbackService apprenticeFeedbackService
            , IApprenticeFeedbackSessionService sessionService
        )
        {
            _logger = logger;
            _apprenticeFeedbackService = apprenticeFeedbackService;
            _sessionService = sessionService;
        }


        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user)
        {
            try
            {
                TrainingProviderItems = await _apprenticeFeedbackService.GetTrainingProviders(user.ApprenticeId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to read training provider data from outer api.", ex);
                return Redirect("Error");
            }

            if (TrainingProviderItems.ContainsCountItems(1))
            {
                var provider = TrainingProviderItems.First();

                var feedbackContext = new FeedbackContext()
                {
                    UkPrn = provider.Ukprn,
                    ProviderName = provider.Name,
                    FeedbackEligibility = provider.FeedbackEligibility,
                    TimeWindow = provider.TimeWindow,
                    SignificantDate = provider.SignificantDate,
                    ProviderCount = 1,
                };
                _sessionService.SetFeedbackContext(feedbackContext);

                if (provider.FeedbackEligibility == FeedbackEligibility.Allow)
                {
                    return Redirect($"/start/{provider.Ukprn}");
                }

                return Redirect("/status");
            }

            FeedbackRate = _apprenticeFeedbackService.RecentDenyPeriod.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month);
            FeedbackInitialDenyPeriod = _apprenticeFeedbackService.InitialDenyPeriod.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month);
            FeedbackFinalAllowPeriod = _apprenticeFeedbackService.FinalAllowPeriod.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month);
            return Page();
        }
    }
}
