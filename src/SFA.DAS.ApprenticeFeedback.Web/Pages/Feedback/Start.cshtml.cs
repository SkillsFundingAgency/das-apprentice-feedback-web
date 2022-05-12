using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    public class StartModel : PageModel, IHasBackLink
    {
        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;
        private readonly IApprenticeFeedbackSessionService _sessionService;
        private readonly Domain.Interfaces.IUrlHelper _urlHelper;

        public string Backlink => "/";
        public string ProviderName { get; set;  }
        public string FindApprenticeshipUrl { get; set; }

        public StartModel(IApprenticeFeedbackSessionService sessionService, 
        IApprenticeFeedbackService apprenticeFeedbackService,
        Domain.Interfaces.IUrlHelper urlHelper)
        {
            _sessionService = sessionService;
            _apprenticeFeedbackService = apprenticeFeedbackService;
            _urlHelper = urlHelper;
        }

        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user, int ukprn)
        {
            var provider = await _apprenticeFeedbackService.GetTrainingProvider(user.ApprenticeId, ukprn);
            if (null == provider)
            {
                return Redirect("/Error");
            }

            // Safety check incase the URL has been manipulated with a valid but ineligible ukprn
            if(provider.FeedbackEligibility != FeedbackEligibility.Allow)
            {
                var errorFeedbackContext = new FeedbackContext()
                {
                    UkPrn = provider.Ukprn,
                    LarsCode = provider.LarsCode,
                    ApprenticeFeedbackTargetId = provider.ApprenticeFeedbackTargetId,
                    ProviderName = provider.Name,
                    FeedbackEligibility = provider.FeedbackEligibility,
                    TimeWindow = provider.TimeWindow,
                    SignificantDate = provider.SignificantDate,
                };
                _sessionService.SetFeedbackContext(errorFeedbackContext);
                return Redirect("/status");
            }

            var feedbackContext = FeedbackContext.CreateFrom(provider);
            feedbackContext.FeedbackAttributes = new List<FeedbackAttribute>(await _apprenticeFeedbackService.GetFeedbackAttributes());
            _sessionService.SetFeedbackContext(feedbackContext);

            ProviderName = feedbackContext.ProviderName;
            FindApprenticeshipUrl = _urlHelper.FindApprenticeshipTrainingFeedbackUrl(feedbackContext.UkPrn, feedbackContext.LarsCode);
            return Page();
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("FeedbackAttributes");
        }
    }
}