using SFA.DAS.ApprenticeFeedback.Domain.Models;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Web.Components.AttributeRadios
{
    public class AttributeRadiosModel
    {
        public List<FeedbackAttribute> Attributes { get; set; }
        public string HeadingHtml { get; set; }
        public int? SelectedAttributeId { get; set; }
    }
}
