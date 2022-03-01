using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class FeedbackAttributesModel : PageModel, IHasBackLink
    {
        private IApprenticeFeedbackSessionService _sessionService;

        public FeedbackAttributesModel(IApprenticeFeedbackSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public string TrainingProvider { get; set; }

        [BindProperty]
        public List<FeedbackAttribute> FeedbackAttributes { get; set; }

        [BindProperty]
        public bool? Editing { get; set; }

        public string Backlink => Editing.HasValue && Editing.Value == true ? "check-answers" : "";

        public void OnGet(bool? edit)
        {
            Editing = edit;

            var feedbackRequest = _sessionService.GetFeedbackRequest();

            TrainingProvider = feedbackRequest.TrainingProvider;
            FeedbackAttributes = feedbackRequest.FeedbackAttributes;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.ErrorCount > 1)
                {
                    ModelState.AddModelError("MultipleErrorSummary", "Select Agree or Disagree");
                }
                
                return Page();
            }

            var feedbackRequest = _sessionService.GetFeedbackRequest();

            feedbackRequest.FeedbackAttributes = FeedbackAttributes;

            _sessionService.UpdateFeedbackRequest(feedbackRequest);

            if (Editing.HasValue && Editing.Value == true)
            {
                return RedirectToPage("CheckYourAnswers");
            }

            return RedirectToPage("OverallRating");
        }
    }
}
