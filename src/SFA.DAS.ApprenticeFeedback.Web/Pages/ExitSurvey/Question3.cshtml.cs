using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class Question3Model : ExitSurveyContextPageModel, IHasBackLink
    {
        public string Backlink => (ExitSurveyContext.CheckingAnswers) ? $"./checkyouranswers" : $"./primaryreason";

        [BindProperty]
        [Required(ErrorMessage = "Select which of the following would have made you stay on the apprenticeship")]
        public List<FeedbackAttribute> ReasonAttributes { get; set; }

        // This ID is set by the post deployment script in the inner API database project
        private const int NoneAttributeId = 59;

        public Question3Model(IExitSurveySessionService sessionService
            , IApprenticeFeedbackService apprenticeFeedbackService)
            : base(sessionService, apprenticeFeedbackService)
        {
        }

        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user)
        {
            // Get the attributes for this page.
            ReasonAttributes = new List<FeedbackAttribute>(await ApprenticeFeedbackService.GetExitSurveyAttributes(ExitSurveyAttributeCategory.RemainFactors));

            // Load the data from the session

            var selectedReasonAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.RemainFactors).ToList();
            foreach (var sessionAttribute in selectedReasonAttributes)
            {
                var a = ReasonAttributes.Find(a => a.Id == sessionAttribute.Id);
                a.Value = true;
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var selectedReasonAttributes = ReasonAttributes.Where(a => a.Value).ToList();
            int selectedCount = selectedReasonAttributes.Count();

            if (0 == selectedCount)
            {
                ModelState.AddModelError("MultipleErrorSummary", "Select the factors that would have made you stay or select 'None of these would have made me stay'");
            }
            if(selectedCount > 1)
            {
                var exclusiveAttribute = selectedReasonAttributes.FirstOrDefault(a => a.Id == NoneAttributeId);
                if(null != exclusiveAttribute)
                {
                    ModelState.AddModelError("MultipleErrorSummary", "Select the factors that would have made you stay or select 'None of these would have made me stay'");  // Select which of the following would have made you stay on the apprenticeship
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Reset any previously selected attributes in the session
            ExitSurveyContext.Clear(ExitSurveyAttributeCategory.RemainFactors);
            // Add the selected attributes to the session
            foreach (var a in selectedReasonAttributes)
            {
                ExitSurveyContext.Attributes.Add(a);
            }
            SaveContext();

            return RedirectToPage("./checkyouranswers");
        }
    }
}