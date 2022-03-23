using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class StartModel : PageModel
    {
        private readonly IApprenticeFeedbackSessionService _sessionService;
        private readonly Domain.Interfaces.IUrlHelper _urlHelper;
        private readonly ILogger<StartModel> _logger;

        public StartModel(IApprenticeFeedbackSessionService sessionService, Domain.Interfaces.IUrlHelper urlHelper, ILogger<StartModel> logger)
        {
            _sessionService = sessionService;
            _urlHelper = urlHelper;
            _logger = logger;
        }

        public string ProviderName { get; set; }
        public string FATLink { get; set; }


        public async Task<IActionResult> OnGet()
        {
            var request = _sessionService.GetFeedbackRequest();

            if (request.Ukprn == 0 || request.LarsCode == 0)
            {
                return RedirectToPage("./Index");
            }
            FATLink = _urlHelper.FATFeedback(request.Ukprn, request.LarsCode);
            ProviderName = request.TrainingProvider;
            return Page();
        }

        public IActionResult OnPost()
        {
            // _sessionService.StartNewFeedbackRequest();

            return RedirectToPage("FeedbackAttributes");
        }
    }
}