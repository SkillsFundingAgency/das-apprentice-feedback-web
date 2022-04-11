using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    [HideNavigationBar]
    public class StartModel : PageModel, IHasBackLink
    {
        private readonly IApprenticeFeedbackSessionService _sessionService;
        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;

        public string Backlink => "/";
        public string ProviderName { get; set;  }
        public string FindApprenticeshipUrl { get; set; }

        public StartModel(IApprenticeFeedbackSessionService sessionService, IApprenticeFeedbackService apprenticeFeedbackService)
        {
            _sessionService = sessionService;
            _apprenticeFeedbackService = apprenticeFeedbackService;
        }

        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user, int ukprn)
        {
            var provider = await _apprenticeFeedbackService.GetTrainingProvider(user.ApprenticeId, ukprn);
            if (null == provider)
            {
                return Redirect("/Error");
            }

            var feedbackContext = FeedbackContext.CreateFrom(provider);
            feedbackContext.FeedbackAttributes = new List<FeedbackAttribute>(await _apprenticeFeedbackService.GetFeedbackAttributes());
            _sessionService.SetFeedbackContext(feedbackContext);

            ProviderName = feedbackContext.ProviderName;
            FindApprenticeshipUrl = $"https://findapprenticeshiptraining.apprenticeships.education.gov.uk/courses/{feedbackContext.LarsCode}/providers/{feedbackContext.UkPrn}";

            return Page();
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("FeedbackAttributes");
        }
    }
}
