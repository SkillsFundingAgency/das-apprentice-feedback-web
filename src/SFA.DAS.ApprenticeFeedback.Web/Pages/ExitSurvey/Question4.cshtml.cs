using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
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
    public class Question4Model : ExitSurveyContextPageModel, IHasBackLink
    {
        public string Backlink => (ExitSurveyContext.CheckingAnswers) ? "./checkyouranswers" : "./question3";
        private readonly string Category = ExitSurveyAttributeCategory.PostApprenticeshipStatus;

        [BindProperty]
        [Required(ErrorMessage = "Select what did you do after leaving your apprenticeship or select 'Something else'")]
        public int? SelectedAttributeId { get; set; }
        [BindProperty]
        public List<ExitSurveyAttribute> Attributes { get; set; }

        public Question4Model(IExitSurveySessionService sessionService,
            IApprenticeFeedbackService apprenticeFeedbackService)
            : base(sessionService, apprenticeFeedbackService)
        {
        }

        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user)
        {
            Attributes = (await ApprenticeFeedbackService.GetExitSurveyAttributes(Category)).ToList();

            SelectedAttributeId = ExitSurveyContext.Attributes
                .FirstOrDefault(a => a.Category == Category)?.Id;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var selectedAttribute = Attributes.FirstOrDefault(a => a.Id == SelectedAttributeId.Value);

            ExitSurveyContext.Clear(Category);
            ExitSurveyContext.Attributes.Add(selectedAttribute);
            SaveContext();
            return RedirectToPage("./checkyouranswers");
        }
    }
}