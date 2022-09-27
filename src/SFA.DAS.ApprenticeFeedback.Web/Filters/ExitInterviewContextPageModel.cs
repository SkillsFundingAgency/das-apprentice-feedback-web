using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;

namespace SFA.DAS.ApprenticeFeedback.Web.Filters
{
    public class ExitSurveyContextPageModel : PageModel
    {
        private readonly IExitSurveySessionService _sessionService;
        private ExitSurveyContext _ExitSurveyContext;

        public ExitSurveyContext ExitSurveyContext { get { return _ExitSurveyContext; } }

        public ExitSurveyContextPageModel(IExitSurveySessionService sessionService)
        {
            _sessionService = sessionService;
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

            base.OnPageHandlerExecuting(context);
        }

        public void SaveContext()
        {
            _sessionService.SetExitSurveyContext(_ExitSurveyContext);
        }
    }
}
