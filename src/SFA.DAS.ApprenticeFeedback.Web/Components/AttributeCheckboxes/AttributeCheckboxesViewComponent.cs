using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Components.AttributeCheckboxes
{
    public class AttributeCheckboxesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<ExitSurveyAttribute> attributes, string attributesModelName, string headingHtml, bool lastIsExclusive)
        {
            var model = new AttributeCheckboxesModel()
            {
                Attributes = attributes,
                AttributesModelName = attributesModelName,
                HeadingHtml = headingHtml,
                LastIsExclusive = lastIsExclusive
            };
            return View("~/Components/AttributeCheckboxes/AttributeCheckboxes.cshtml", model);
        }
    }
}
