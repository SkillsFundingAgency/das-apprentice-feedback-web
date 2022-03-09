using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    [HideNavigationBar]
    public class FeedbackAttributesModel : PageModel, IHasBackLink
    {
        private IApprenticeFeedbackSessionService _sessionService;
        private IApprenticeFeedbackService _apprenticeFeedbackService;

        public FeedbackAttributesModel(IApprenticeFeedbackSessionService sessionService,
            IApprenticeFeedbackService apprenticeFeedbackService)
        {
            _sessionService = sessionService;
            _apprenticeFeedbackService = apprenticeFeedbackService;
        }

        public string TrainingProvider { get; set; }

        [BindProperty]
        public List<FeedbackAttribute> FeedbackAttributes { get; set; }

        [BindProperty]
        public bool? Editing { get; set; }

        public string Backlink => Editing.HasValue && Editing.Value == true ? "check-answers" : "";

        public async Task OnGet(bool? edit)
        {
            Editing = edit;

            var feedbackRequest = _sessionService.GetFeedbackRequest();

            TrainingProvider = feedbackRequest.TrainingProvider;

            if (feedbackRequest.FeedbackAttributes == null)
            {
                var attributes = await _apprenticeFeedbackService.GetTrainingProviderAttributes();
                feedbackRequest.FeedbackAttributes = attributes;
                _sessionService.UpdateFeedbackRequest(feedbackRequest);
            }
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
