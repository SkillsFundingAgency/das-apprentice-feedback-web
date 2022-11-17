﻿using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Components.AttributeRadios
{
    public class AttributeRadiosViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<FeedbackAttribute> attributes, string headingHtml)
        {
            var model = new AttributeRadiosModel()
            {
                Attributes = new List<FeedbackAttribute>(attributes),
                HeadingHtml = headingHtml
            };
            return View("~/Components/AttributeRadios/AttributeRadios.cshtml", model);
        }
    }
}
