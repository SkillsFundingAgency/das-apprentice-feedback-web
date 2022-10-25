using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using System;

namespace SFA.DAS.ApprenticeFeedback.Web.Filters
{
    public class ExitSurveyContextPageModel : PageModel
    {
        private readonly IExitSurveySessionService _sessionService;
        private readonly UserJourney _currentPageJourney;
        private ExitSurveyContext _ExitSurveyContext;

        public ExitSurveyContext ExitSurveyContext { get { return _ExitSurveyContext; } }

        public ExitSurveyContextPageModel(IExitSurveySessionService sessionService, UserJourney currentPageJourney)
        {
            _sessionService = sessionService;
            _currentPageJourney = currentPageJourney;
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

                // If the survey is completed and we are attempting to access any page other than
                // the completed page, then redirect to completed
                if (_ExitSurveyContext.DateTimeCompleted.HasValue 
                    && !context.HttpContext.Request.Path.StartsWithSegments("/exit/complete")
                    && !context.HttpContext.Request.Path.StartsWithSegments("/exit/incorrectcomplete"))
                {
                    if(_ExitSurveyContext.DidNotCompleteApprenticeship.Value) {
                        context.Result = Redirect("/exit/complete");
                    }
                    else
                    {
                        context.Result = Redirect("/exit/incorrectcomplete");
                    }
                    return;
                }

                // If we are attempting to access a page that is on the opposite user journey
                // then redirect to the journey decision page
                if(_currentPageJourney != _ExitSurveyContext.UserJourney &&
                    _currentPageJourney != UserJourney.Finished)
                {
                    if ( (_ExitSurveyContext.UserJourney != UserJourney.Start 
                        ))
                    {
                        _ExitSurveyContext.UserJourney = UserJourney.Start;
                        SaveContext();

                        context.Result = Redirect("/exit/question1");
                        return;
                    }

                    // @ToDo:
                    // stop the user from jumping to the last page (happy or unhappy path)
                    // without having answered all the questions
                }
            }

            _ExitSurveyContext.UserJourney = _currentPageJourney;
            SaveContext();

            base.OnPageHandlerExecuting(context);
        }

        public void SaveContext()
        {
            _sessionService.SetExitSurveyContext(_ExitSurveyContext);
        }
    }
}
