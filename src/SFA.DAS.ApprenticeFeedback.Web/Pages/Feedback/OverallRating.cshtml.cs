using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class OverallRatingModel : PageModel, IHasBackLink
    {
        private IApprenticeFeedbackSessionService _sessionService;

        public OverallRatingModel(IApprenticeFeedbackSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [BindProperty]
        public string TrainingProvider { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Select a rating")]
        public OverallRating? OverallRating { get; set; }

        [BindProperty]
        public bool? Editing { get; set; }

        public string Backlink => Editing.HasValue && Editing.Value == true ? "/check-answers" : "feedback-attributes";

        public void OnGet(bool? edit)
        { 
            Editing = edit;

            var feedbackRequest = _sessionService.GetFeedbackRequest();

            TrainingProvider = feedbackRequest.TrainingProvider;
            OverallRating = feedbackRequest.OverallRating;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var feedbackRequest = _sessionService.GetFeedbackRequest();

            feedbackRequest.OverallRating = OverallRating;

            _sessionService.UpdateFeedbackRequest(feedbackRequest);

            return RedirectToPage("CheckYourAnswers");
        }
    }
}
