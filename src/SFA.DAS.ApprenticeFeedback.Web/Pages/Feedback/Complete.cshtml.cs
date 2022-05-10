using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class CompleteModel : FeedbackContextPageModel
    {
        private readonly Domain.Interfaces.IUrlHelper _urlHelper;

        public CompleteModel(IApprenticeFeedbackSessionService sessionService, Domain.Interfaces.IUrlHelper urlHelper)
            :base(sessionService) 
        {
            _urlHelper = urlHelper;
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
            ReturnToDashboardUrl = "/";
            HelpAndSupportUrl = "#";
            MentalHealthSupportUrl = "#";
        }
    }
}
