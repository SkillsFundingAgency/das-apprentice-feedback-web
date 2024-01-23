using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NServiceBus;
using SFA.DAS.ApprenticeFeedback.Application.Settings;
using SFA.DAS.ApprenticeFeedback.Messages.Events;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<IActionResult> OnGet(string linkName, string templateName, long feedbackTransactionId, Guid apprenticeFeedbackTargetId)
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

                    return Redirect(GetUrlWithTemplateNameQueryParameter(matchingLink, templateName));
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

        private string GetUrlWithTemplateNameQueryParameter(EngagementLink engagementLink, string templateName)
        {
            var uriBuilder = new UriBuilder(new Uri(engagementLink.Url));

            NameValueCollection queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (!string.IsNullOrEmpty(templateName))
            {
                queryParameters.Add("utm_source", "apprentice_feedback");
                queryParameters.Add("utm_medium", "email");
                queryParameters.Add("utm_campaign", "engagement");
                queryParameters.Add("utm_content", $"template_{templateName}");
            }
            uriBuilder.Query = queryParameters.ToString();

            string path = uriBuilder.Path != "/" ? uriBuilder.Path : string.Empty;
            string query = queryParameters.Count > 0 ? $"?{queryParameters}" : string.Empty;

            return $"{uriBuilder.Scheme}://{uriBuilder.Host}{path}{query}";
        }
    }
}
