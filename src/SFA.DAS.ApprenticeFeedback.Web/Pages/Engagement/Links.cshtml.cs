using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.ApprenticeFeedback.Application.Settings;
using SFA.DAS.ApprenticeFeedback.Messages.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.Engagement
{
    [AllowAnonymous]
    public class LinksModel : PageModel
    {
        private readonly IMessageSession _eventPublisher;
        private readonly ILogger<LinksModel> _logger;
        private readonly AppSettings _appSettings;

        public LinksModel(IMessageSession eventPublisher, ILogger<LinksModel> logger, AppSettings appSettings)
        {
            _eventPublisher = eventPublisher;
            _logger = logger;
            _appSettings = appSettings;
        }

        public async Task<IActionResult> OnGet(string linkName, long feedbackTransactionId, Guid apprenticeFeedbackTargetId)
        {
            try
            {
                var matchingLink = _appSettings.EngagementLinks.FirstOrDefault(p => p.Name == linkName);

                if (matchingLink != null && !string.IsNullOrEmpty(matchingLink.Url))
                {
                    await _eventPublisher.Publish(new ApprenticeEmailClickEvent
                    {
                        FeedbackTransactionId = feedbackTransactionId,
                        ApprenticeFeedbackTargetId = apprenticeFeedbackTargetId,
                        Link = matchingLink.Url,
                        Linkname = linkName,
                        ClickedOn = DateTime.UtcNow
                    });

                    return Redirect(matchingLink.Url);
                }
                else
                {
                    _logger.LogWarning($"The engagement link '{linkName}' is not configured or has an empty Url.");
                    return Page();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Unable to publish apprentice email click event for engagement link '{linkName}'.");
                throw;
            }
        }
    }
}
