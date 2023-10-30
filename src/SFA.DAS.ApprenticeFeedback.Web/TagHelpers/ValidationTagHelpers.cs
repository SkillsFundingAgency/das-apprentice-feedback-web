using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SFA.DAS.ApprenticeFeedback.Web.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "validation-row-status")]
    public class ValidationRowHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; } = null!;

        public string PropertyName { get; set; } = null!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PropertyIsInvalid())
            {
                output.Attributes.SetAttribute("class", $"{output.Attributes["class"]?.Value} govuk-form-group--error");
            }
        }

        bool PropertyIsInvalid()
        {
            return ViewContext?.ModelState[PropertyName]?.ValidationState == ModelValidationState.Invalid;
        }
    }
}
