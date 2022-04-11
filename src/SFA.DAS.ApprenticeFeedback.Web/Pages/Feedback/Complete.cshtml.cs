using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class CompleteModel : FeedbackContextPageModel
    {
        public CompleteModel(IApprenticeFeedbackSessionService sessionService)
            :base(sessionService) { }

        public OverallRating OverallRating { get; set; }
        public bool IsPoorlyRated => OverallRating == OverallRating.Poor || OverallRating == OverallRating.VeryPoor;
        public string ReturnToDashboardUrl { get; set; }
        public string HelpAndSupportUrl { get; set; }
        public string MentalHealthSupportUrl { get; set; }

        public void OnGet()
        {
            OverallRating = FeedbackContext.OverallRating.Value;
            ReturnToDashboardUrl = "/";
            HelpAndSupportUrl = "#";
            MentalHealthSupportUrl = "#";
        }
    }
}
