using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Components.AttributeRadios
{
    public class AttributeRadiosViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<ExitSurveyAttribute> attributes, string headingHtml, int? selectedAttributeId)
        {
            var model = new AttributeRadiosModel()
            {
                Attributes = new List<ExitSurveyAttribute>(attributes),
                HeadingHtml = headingHtml,
                SelectedAttributeId = selectedAttributeId
            };
            return View("~/Components/AttributeRadios/AttributeRadios.cshtml", model);
        }
    }
}
