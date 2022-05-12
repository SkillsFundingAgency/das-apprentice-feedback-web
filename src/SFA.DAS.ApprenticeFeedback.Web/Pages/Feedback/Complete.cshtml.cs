using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    public class CompleteModel : FeedbackContextPageModel
    {
        private readonly Domain.Interfaces.IUrlHelper _urlHelper;
        private readonly NavigationUrlHelper _navigationUrlHelper;

        public CompleteModel(IApprenticeFeedbackSessionService sessionService, Domain.Interfaces.IUrlHelper urlHelper, NavigationUrlHelper navigationUrlHelper)
            :base(sessionService) 
        {
            _urlHelper = urlHelper;
            _navigationUrlHelper = navigationUrlHelper;
        }

        public OverallRating OverallRating { get; set; }
        public bool IsPoorlyRated => OverallRating == OverallRating.Poor || OverallRating == OverallRating.VeryPoor;
        public string ReturnToDashboardUrl { get; set; }
        public string HelpAndSupportUrl { get; set; }
        public string MentalHealthSupportUrl { get; set; }
        public string FindApprenticeshipUrl { get; set; }

        public void OnGet()
        {
            FindApprenticeshipUrl = _urlHelper.FindApprenticeshipTrainingFeedbackUrl(FeedbackContext.UkPrn, FeedbackContext.LarsCode);
            OverallRating = FeedbackContext.OverallRating.Value;
            ReturnToDashboardUrl = _navigationUrlHelper.Generate(NavigationSection.Home);
            HelpAndSupportUrl = _navigationUrlHelper.Generate(NavigationSection.HelpAndSupport);
            MentalHealthSupportUrl = "#";
        }
    }
}
