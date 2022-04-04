﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Hooks
{
    public class TestActionResult
    {
        public IActionResult LastActionResult { get; private set; }
        public PageResult LastPageResult { get; private set; }
        public RedirectToPageResult LastRedirectToPageResult { get; private set; }
        public RedirectResult LastRedirectResult { get; private set; }

        public Exception LastException { get; private set; }

        public void SetActionResult(IActionResult actionResult)
        {
            LastActionResult = actionResult;
            
            if (actionResult is PageResult pageResult)
            {
                LastPageResult = pageResult;
            }
            else if (actionResult is RedirectToPageResult redirectToPageResult)
            {
                LastRedirectToPageResult = redirectToPageResult;
            }
            else if (actionResult is RedirectResult redirectResult)
            {
                LastRedirectResult = redirectResult;
            }
        }

        public void SetException(Exception exception)
        {
            LastException = exception;
        }
    }
}
