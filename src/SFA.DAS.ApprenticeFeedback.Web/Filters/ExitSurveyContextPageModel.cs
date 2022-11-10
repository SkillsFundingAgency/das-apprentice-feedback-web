using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Filters
{
    public class ExitSurveyContextPageModel : PageModel
    {
        private readonly IExitSurveySessionService _sessionService;
        private ExitSurveyContext _ExitSurveyContext;
        private readonly IApprenticeFeedbackService _apprenticeFeedbackService;

        public ExitSurveyContext ExitSurveyContext { get { return _ExitSurveyContext; } }
        public IApprenticeFeedbackService ApprenticeFeedbackService { get { return _apprenticeFeedbackService; } }

        public ExitSurveyContextPageModel(IExitSurveySessionService sessionService
            , IApprenticeFeedbackService apprenticeFeedbackService
            )
        {
            _sessionService = sessionService;
            _apprenticeFeedbackService = apprenticeFeedbackService;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            _ExitSurveyContext = _sessionService.GetExitSurveyContext();
            if (null == _ExitSurveyContext)
            {
                if(!context.HttpContext.Request.Path.StartsWithSegments("/exit/start"))
                {
                    context.Result = Redirect("/");
                    return;
                }
                
                _ExitSurveyContext = new ExitSurveyContext();
                SaveContext();
            }
            else
            {
                // No valid apprentice feedback target ID in the context so do not proceed.
                if(Guid.Empty == _ExitSurveyContext.ApprenticeFeedbackTargetId)
                {
                    context.Result = Redirect("/");
                    return;
                }

                // Make sure the survey has not already been completed

                if (!ExitSurveyContext.SurveyCompleted.HasValue)
                {
                    var exitSurvey = Task.Run(async () => await _apprenticeFeedbackService.GetExitSurveyForFeedbackTarget(ExitSurveyContext.ApprenticeFeedbackTargetId.Value)).GetAwaiter().GetResult();
                    if(null != exitSurvey)
                    {
                        ExitSurveyContext.SurveyCompleted = true;
                        ExitSurveyContext.DateTimeCompleted = exitSurvey.DateTimeCompleted;
                        ExitSurveyContext.DidNotCompleteApprenticeship = exitSurvey.DidNotCompleteApprenticeship;
                        SaveContext();
                    }
                    else
                    {
                        ExitSurveyContext.SurveyCompleted = false;
                        SaveContext();
                    }
                }
                if(ExitSurveyContext.SurveyCompleted.Value)
                {
                    // Redirect if we're not on the completed page
                    if (!context.HttpContext.Request.Path.StartsWithSegments("/exit/complete") &&
                        !context.HttpContext.Request.Path.StartsWithSegments("/exit/incorrectcomplete"))
                    {
                        if(ExitSurveyContext.DidNotCompleteApprenticeship.Value)
                        {
                            context.Result = Redirect("/exit/complete");
                        }
                        else
                        {
                            context.Result = Redirect("/exit/incorrectcomplete");
                        }
                    }
                    return;
                }

                // If the session has no attributes set and we are not on the start page or question 1
                // then the user may have manipulated the URL directly in the browser
                // so let's redirect to the start page for safety.
                if (!context.HttpContext.Request.Path.StartsWithSegments("/exit/start")
                    && !context.HttpContext.Request.Path.StartsWithSegments("/exit/question1"))
                {
                    if(!ExitSurveyContext.Attributes.Any())
                    {
                        context.Result = Redirect($"/exit/start/{ExitSurveyContext.ApprenticeFeedbackTargetId}");
                        return;
                    }
                }

                // If we're on check your answers and there are no answers or no primary reason has been set
                // then the user may have manipulated the URL directly in the browser
                // so redirect to question 2 for safety.
                if (context.HttpContext.Request.Path.StartsWithSegments("/exit/checkyouranswers"))
                {
                    if (ExitSurveyContext.Attributes.Count() < 2)
                    {
                        context.Result = Redirect($"/exit/question2");
                        return;
                    }
                    if (!ExitSurveyContext.PrimaryReason.HasValue)
                    {
                        context.Result = Redirect($"/exit/primaryreason");
                        return;
                    }
                }
            }

            base.OnPageHandlerExecuting(context);
        }

        public void SaveContext()
        {
            _sessionService.SetExitSurveyContext(_ExitSurveyContext);
        }
    }
}
