using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class IncorrectReasonModel : ExitSurveyContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Select a reason")]
        public string ReasonForIncorrect { get; set; }
        public string[] Reasons = new[]
        {
            "I am still doing my apprenticeship",
            "I completed the apprenticeship and had my end point assessment",
            "I completed my training and I'm waiting for the end point assessment",
            "I never started an apprenticeship"
        };

        [BindProperty]
        public bool AllowContact { get; set; }

        public string Backlink => $"./question1";

        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;

        public IncorrectReasonModel(IExitSurveySessionService sessionService, 
            IApprenticeFeedbackService apprenticeFeedbackService)
            : base(sessionService)
        {
            _apprenticeFeedbackService = apprenticeFeedbackService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitSurveyContext.ReasonForIncorrect = ReasonForIncorrect;
            ExitSurveyContext.AllowContact = AllowContact;
            SaveContext();

            // Save 
            var request = new PostSubmitExitSurvey
            {
                ApprenticeFeedbackTargetId = ExitSurveyContext.ApprenticeFeedbackTargetId,
                DidNotCompleteApprenticeship = ExitSurveyContext.DidNotCompleteApprenticeship.Value,
                IncompletionReason = ExitSurveyContext.IncompletionReason,
                IncompletionFactor = ExitSurveyContext.IncompletionFactor,
                ReasonForIncorrect = ExitSurveyContext.ReasonForIncorrect,
                RemainedReason = ExitSurveyContext.RemainedReason,
                AllowContact = ExitSurveyContext.AllowContact
            };
            await _apprenticeFeedbackService.SubmitExitSurvey(request);

            // Q. Do we need to somehow prevent a resubmit?

            return RedirectToPage("./incorrectcomplete");
        }

    }
}
