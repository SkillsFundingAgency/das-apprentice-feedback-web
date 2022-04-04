using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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
        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;
        private readonly IApprenticeFeedbackSessionService _apprenticeFeedbackSessionService;
        private readonly NavigationUrlHelper _urlHelper;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IApprenticeFeedbackService apprenticeFeedbackService, IApprenticeFeedbackSessionService apprenticeFeedbackSessionService, NavigationUrlHelper urlHelper, ILogger<IndexModel> logger)
        {
            _apprenticeFeedbackService = apprenticeFeedbackService;
            _apprenticeFeedbackSessionService = apprenticeFeedbackSessionService;
            _urlHelper = urlHelper;
            _logger = logger;
        }

        [BindProperty]
        public long SelectedProviderUkprn { get; set; }

        public List<TrainingProvider> PotentialProviders { get; set; }
       
        public string DashboardLink { get; set; }
        public string Backlink => DashboardLink;

        public async Task<IActionResult> OnGet()//[FromServices] AuthenticatedUser user
        {
            DashboardLink = _urlHelper.Generate(NavigationSection.Home);

            PotentialProviders = await _apprenticeFeedbackService.GetTrainingProviders(user.ApprenticeId);

            if (PotentialProviders.Count == 1)
            {
                var onlyProvider = PotentialProviders.Single();

                if (onlyProvider.IsValidForFeedback)
                {
                    _apprenticeFeedbackSessionService.StartNewFeedbackRequest(onlyProvider.Name, onlyProvider.Ukprn, onlyProvider.GetMostRecentLarsCode());

                    return RedirectToPage("Feedback/Start");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost([FromServices] AuthenticatedUser user)
        {
            var provider = await _apprenticeFeedbackService.GetTrainingProvider(user.ApprenticeId, SelectedProviderUkprn);

            if (provider.IsValidForFeedback)
            {
                var larsCode = provider.GetMostRecentLarsCode();

                _apprenticeFeedbackSessionService.StartNewFeedbackRequest(provider.Name, provider.Ukprn, larsCode);

                return RedirectToPage("Feedback/Start");
            }

            PotentialProviders = new List<TrainingProvider> { provider };

            return Page();
        }
    }
}
