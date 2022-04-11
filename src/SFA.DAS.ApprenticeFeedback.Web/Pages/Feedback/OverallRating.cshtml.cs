using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class OverallRatingModel : FeedbackContextPageModel, IHasBackLink
    {
        public OverallRatingModel(IApprenticeFeedbackSessionService sessionService)
            :base(sessionService)
        {
        }

        [BindProperty]
        [Required(ErrorMessage = "Select a rating")]
        public OverallRating? OverallRating { get; set; }

        [BindProperty]
        public bool? Editing { get; set; }

        public string Backlink => Editing.HasValue && Editing.Value == true ? "/check-answers" : "feedback-attributes";

        public IActionResult OnGet(bool? edit)
        { 
            Editing = edit;
            OverallRating = FeedbackContext.OverallRating;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            FeedbackContext.OverallRating = OverallRating;
            SaveFeedbackContext();

            return RedirectToPage("CheckYourAnswers");
        }
    }
}
