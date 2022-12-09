using SFA.DAS.ApprenticeFeedback.Domain.Models;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Web.Components.AttributeCheckboxes
{
    public class AttributeCheckboxesModel
    {
        public List<FeedbackAttribute> Attributes { get; set; }
        public string AttributesModelName { get; set; }
        public string HeadingHtml { get; set; }
        public bool LastIsExclusive { get; set; }
    }
}
