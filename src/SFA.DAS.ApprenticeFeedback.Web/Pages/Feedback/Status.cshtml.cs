using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    public class StatusModel : FeedbackContextPageModel
    {
        public bool IsHappyStatus { get; set; }
        public string Header { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationContentHeading { get; set; }
        public string NotificationContent { get; set; }
        public string ReturnToDashboardUrl { get; set; }

        public StatusModel(IApprenticeFeedbackSessionService sessionService)
            :base(sessionService)
        {
        }

        public IActionResult OnGet()
        {
            Header = "Feedback on your training provider";
            ViewData["Title"] = Header;
            NotificationTitle = "Important";
            ReturnToDashboardUrl = "/";

            switch(FeedbackContext.FeedbackEligibility)
            {
                // Happy messages
                case Domain.Models.Feedback.FeedbackEligibility.Deny_HasGivenFeedbackRecently:
                    IsHappyStatus = true;
                    NotificationContentHeading = $"You have already given feedback recently.";
                    NotificationContent = $"You can give feedback again on or after {FeedbackContext.SignificantDate.Value.ToString("d MMMM yyyy")}.";
                    break;
                case Domain.Models.Feedback.FeedbackEligibility.Deny_HasGivenFinalFeedback:
                    IsHappyStatus = true;
                    NotificationContentHeading = $"You have already given your final feedback on this training provider.";
                    NotificationContent = $"You can only give feedback on them if you start a new apprenticeship with them. If you start a new apprenticeship with a different training provider, you will be able to give feedback on them {FeedbackContext.TimeWindow.Value.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month)} after your planned training start date.";
                    break;

                // Unhappy messages
                case Domain.Models.Feedback.FeedbackEligibility.Deny_TooSoon:
                    NotificationContentHeading = $"We are unable to accept feedback on your training provider until {FeedbackContext.TimeWindow.Value.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month)} after your planned training start date.";
                    NotificationContent = $"You can start giving feedback on or after {FeedbackContext.SignificantDate.Value.ToString("d MMMM yyyy")}.";
                    break;
                case Domain.Models.Feedback.FeedbackEligibility.Deny_TooLateAfterPassing:
                    NotificationContentHeading = $"It is over {FeedbackContext.TimeWindow.Value.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month)} since you completed your apprenticeship and we are unable to accept feedback after this time.";
                    break;
                case Domain.Models.Feedback.FeedbackEligibility.Deny_TooLateAfterWithdrawing:
                    NotificationContentHeading = $"It is over {FeedbackContext.TimeWindow.Value.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Month)} since you withdrew from your apprenticeship and we are unable to accept feedback after this time.";
                    break;
            }

            return Page();
        }
    }
}
