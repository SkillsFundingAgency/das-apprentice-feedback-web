using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Feedback
{
    public class FeedbackAttributesModel : FeedbackContextPageModel, IHasBackLink
    {

        public FeedbackAttributesModel(IApprenticeFeedbackSessionService sessionService)
            : base(sessionService)
        {
        }

        [BindProperty]
        public List<FeedbackAttribute> FeedbackAttributes { get; set; }

        [BindProperty]
        public bool? Editing { get; set; }

        public string Backlink => Editing.HasValue && Editing.Value == true ? "check-answers" : $"start/{FeedbackContext.UkPrn}";

        public async Task<IActionResult> OnGet(bool? edit)
        {
            FeedbackAttributes = FeedbackContext.FeedbackAttributes;
            Editing = edit;
            return Page();
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

            FeedbackContext.FeedbackAttributes = FeedbackAttributes;
            SaveFeedbackContext();

            if (Editing.HasValue && Editing.Value == true)
            {
                return RedirectToPage("CheckYourAnswers");
            }
            return RedirectToPage("OverallRating");
        }
    }
}
