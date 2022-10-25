using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class CheckYourAnswersModel : ExitSurveyContextPageModel, IHasBackLink
    {
        [BindProperty]
        public bool AllowContact { get; set; }

        public string Backlink => $"./question4";

        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CheckYourAnswersModel(IExitSurveySessionService sessionService, 
            IApprenticeFeedbackService apprenticeFeedbackService,
            IDateTimeProvider dateTimeProvider)
            : base(sessionService, Domain.Models.ExitSurvey.UserJourney.DidNotComplete)
        {
            _apprenticeFeedbackService = apprenticeFeedbackService;
            _dateTimeProvider = dateTimeProvider;
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            // Will need a model decorator that works out if the apprentice has withdrawn
            // and hasn't filled in an exit survey, otherwise redirect,
            // will take in a feedback target guid in the Url as well.

            ExitSurveyContext.CheckingAnswers = true;
            SaveContext();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitSurveyContext.AllowContact = AllowContact;

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

            return RedirectToPage("./complete");
        }
    }
}