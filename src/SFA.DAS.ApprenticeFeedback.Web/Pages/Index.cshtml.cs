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
        public TrainingProvider SelectedProvider { get; set; }
       
        public string DashboardLink { get; set; }
        public string Backlink => DashboardLink;

        public async Task<IActionResult> OnGet()//[FromServices] AuthenticatedUser user
        {
            DashboardLink = _urlHelper.Generate(NavigationSection.Home);

            var apprenticeId = Guid.NewGuid();

            var providers = await _apprenticeFeedbackService.GetTrainingProviders(apprenticeId);

            if (providers.Count > 1)
            {
                PotentialProviders = providers;
            }
            else
            {
                SelectedProvider = providers.FirstOrDefault();

                if (SelectedProvider.IsValidForFeedback)
                {
                    _apprenticeFeedbackSessionService.StartNewFeedbackRequest(SelectedProvider.Name, SelectedProvider.Ukprn, 1);

                    return RedirectToPage("Feedback/Start");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var provider = await _apprenticeFeedbackService.GetTrainingProvider(Guid.NewGuid(), SelectedProviderUkprn);

            if (provider.IsValidForFeedback)
            {
                var larsCode = provider.GetMostRecentLarsCode();

                _apprenticeFeedbackSessionService.StartNewFeedbackRequest(provider.Name, provider.Ukprn, larsCode);

                return RedirectToPage("Feedback/Start");
            }

            SelectedProvider = provider;

            return Page();
        }
    }
}
