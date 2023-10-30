using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;

namespace SFA.DAS.ApprenticeFeedback.Web.Filters
{
    public class FeedbackContextPageModel : PageModel
    {
        private readonly IApprenticeFeedbackSessionService _sessionService;

        private FeedbackContext _feedbackContext;
        protected FeedbackContext FeedbackContext { get { return _feedbackContext; } }

        public string ProviderName { get; set; }

        public FeedbackContextPageModel(IApprenticeFeedbackSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            _feedbackContext = _sessionService.GetFeedbackContext();
            if (null == _feedbackContext)
            {
                context.Result = Redirect("/");
                return;
            }

            ProviderName = _feedbackContext.ProviderName;

            base.OnPageHandlerExecuting(context);
        }

        public void SaveFeedbackContext()
        {
            _sessionService.SetFeedbackContext(_feedbackContext);
        }
    }
}
