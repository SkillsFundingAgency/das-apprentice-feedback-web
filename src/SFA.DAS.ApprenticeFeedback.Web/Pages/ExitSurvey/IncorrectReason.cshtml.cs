using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
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
        public IEnumerable<string> Reasons { get { return _reasons; } }

        [BindProperty]
        public bool AllowContact { get; set; }

        public string Backlink => $"./question1";

        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;

        private readonly string[] _reasons = new[]
        {
            "I am still doing my apprenticeship",
            "I am waiting for my employer to appoint me a new training provider",
            "I completed the apprenticeship and had my end-point assessment",
            "I completed my training and I'm waiting for the end-point assessment",
            "I never started an apprenticeship"
        };

        public IncorrectReasonModel(IExitSurveySessionService sessionService, 
            IApprenticeFeedbackService apprenticeFeedbackService)
            : base(sessionService)
        {
            _apprenticeFeedbackService = apprenticeFeedbackService;
        }

        public IActionResult OnGet()
        {
            return Page();
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
                IncompletionFactor_Caring = ExitSurveyContext.IncompletionFactor_Caring,
                IncompletionFactor_Family = ExitSurveyContext.IncompletionFactor_Family,
                IncompletionFactor_Financial = ExitSurveyContext.IncompletionFactor_Financial,
                IncompletionFactor_Mental = ExitSurveyContext.IncompletionFactor_Mental,
                IncompletionFactor_Other = ExitSurveyContext.IncompletionFactor_Other,
                IncompletionFactor_Physical = ExitSurveyContext.IncompletionFactor_Physical,
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
