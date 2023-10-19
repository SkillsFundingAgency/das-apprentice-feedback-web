using Microsoft.AspNetCore.Mvc.Filters;

namespace SFA.DAS.ApprenticeFeedback.Web.Filters
{
    public class DoesNotRequireIdentityConfirmedFilter : IFilterMetadata
    {
        // this filter doesn't do anything, but is required so that there is a filter for each page globally
    }
}
