using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages
{
    public class IndexModel : PageModel, IHasBackLink
    {
        private readonly NavigationUrlHelper _urlHelper;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(NavigationUrlHelper urlHelper, ILogger<IndexModel> logger)
        {
            _urlHelper = urlHelper;
            _logger = logger;
        }

        public bool IsLessThanThreeMonthsSinceStartDate { get; set; }
        public DateTime? FeedbackStartDate { get; set; }
        public bool IsMoreThanThreeMonthsSincePassing { get; set; }
        public bool IsMoreThanThreeMonthsSinceWithdrawal { get; set; }
        public bool HasRecentlyCompleted { get; set; }
        public bool HasGivenFinalFeedback { get; set; }
        public string DashboardLink { get; set; }

        public string Backlink => DashboardLink;

        public void OnGet()
        {
            DashboardLink = _urlHelper.Generate(NavigationSection.Home);

            // get training providers

            // multiple then redirect to select training provider

            // assign warning or complete status

            // if no warning or complete start session and redirect to start page 
        }
    }
}
