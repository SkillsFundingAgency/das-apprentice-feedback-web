using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class StartModel : ExitSurveyContextPageModel
    {
        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;

        public StartModel(IExitSurveySessionService sessionService, 
            IApprenticeFeedbackService apprenticeFeedbackService)
            :base(sessionService, Domain.Models.ExitSurvey.UserJourney.Start)
        {
            _apprenticeFeedbackService = apprenticeFeedbackService;
        }

        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user, Guid apprenticeFeedbackTargetId)
        {
            // 1. Is the apprenticeFeedbackTargetId valid for this user.ApprenticeId ?
            var apprenticeFeedbackTargets = await _apprenticeFeedbackService.GetApprenticeFeedbackTargets(user.ApprenticeId);
            if(null == apprenticeFeedbackTargets 
                || !apprenticeFeedbackTargets.Any()
                || null == apprenticeFeedbackTargets.FirstOrDefault(aft => aft.Id == apprenticeFeedbackTargetId)
                )
            {
                return RedirectToPage("Invalid");
            }

            // Remember the apprentice feedback target id
            ExitSurveyContext.ApprenticeFeedbackTargetId = apprenticeFeedbackTargetId;
            SaveContext();

            // 2. Is there an exit survey for this apprenticeFeedbackTargetId?
            //    If not, then we can proceed.
            //    If there is, then redirect to a new "you have already completed the survey" page

            var exitSurvey = await _apprenticeFeedbackService.GetExitSurveyForFeedbackTarget(apprenticeFeedbackTargetId);
            if (null != exitSurvey)
            {
                // User has already completed a survey for this feedback target.
                ExitSurveyContext.DateTimeCompleted = exitSurvey.DateTimeCompleted;
                SaveContext();
                return RedirectToPage("Complete");
            }

            return Page();
        }
    }
}