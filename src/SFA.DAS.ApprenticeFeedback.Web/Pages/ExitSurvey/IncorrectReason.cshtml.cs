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
        [Required(ErrorMessage = "Select a reason why our information about you is not correct")]
        public string ReasonForIncorrect { get; set; }
        public IEnumerable<string> Reasons { get { return _reasons; } }

        [BindProperty]
        public bool AllowContact { get; set; }

        public string Backlink => $"./question1";

        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;
        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly string[] _reasons = new[]
        {
            "I am still doing my apprenticeship",
            "I am waiting for my employer to appoint me a new training provider",
            "I completed the apprenticeship and had my end-point assessment",
            "I completed my training and I'm waiting for the end-point assessment",
            "I never started an apprenticeship"
        };

        public IncorrectReasonModel(IExitSurveySessionService sessionService, 
            IApprenticeFeedbackService apprenticeFeedbackService,
            IDateTimeProvider dateTimeProvider)
            : base(sessionService, Domain.Models.ExitSurvey.UserJourney.DidComplete)
        {
            _apprenticeFeedbackService = apprenticeFeedbackService;
            _dateTimeProvider = dateTimeProvider;
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
                IncompletionFactor_None = ExitSurveyContext.IncompletionFactor_None,
                IncompletionFactor_Physical = ExitSurveyContext.IncompletionFactor_Physical,
                ReasonForIncorrect = ExitSurveyContext.ReasonForIncorrect,
                RemainedReason = ExitSurveyContext.RemainedReason,
                AllowContact = ExitSurveyContext.AllowContact
            };
            await _apprenticeFeedbackService.SubmitExitSurvey(request);

            // Prevent a resubmit.
            ExitSurveyContext.DateTimeCompleted = _dateTimeProvider.UtcNow;
            ExitSurveyContext.Submitted = true;
            SaveContext();

            return RedirectToPage("./incorrectcomplete");
        }

    }
}
