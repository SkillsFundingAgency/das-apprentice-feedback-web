using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class SelectTrainingProviderModel : PageModel, IHasBackLink
    {
        private readonly NavigationUrlHelper _urlHelper;

        public SelectTrainingProviderModel(NavigationUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

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
