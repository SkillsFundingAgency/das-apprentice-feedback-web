using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitInterview;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Web.Filters
{
    public class ExitInterviewContextPageModel : PageModel
    {
        private readonly IExitInterviewSessionService _sessionService;
        private ExitInterviewContext _exitInterviewContext;

        public ExitInterviewContext ExitInterviewContext { get { return _exitInterviewContext; } }

        public ExitInterviewContextPageModel(IExitInterviewSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            _exitInterviewContext = _sessionService.GetExitInterviewContext();
            if (null == _exitInterviewContext)
            {
                if(!context.HttpContext.Request.Path.Equals("/exit/start"))
                {
                    context.Result = Redirect("/");
                    return;
                }

                _exitInterviewContext = new ExitInterviewContext();
                SaveContext();
            }

            base.OnPageHandlerExecuting(context);
        }

        public void SaveContext()
        {
            _sessionService.SetExitInterviewContext(_exitInterviewContext);
        }
    }
}
