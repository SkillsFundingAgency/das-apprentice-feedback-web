using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    public class PrimaryReasonModel : ExitSurveyContextPageModel, IHasBackLink
    {
        public string Backlink => (ExitSurveyContext.CheckingAnswers) ? $"./checkyouranswers" : $"./question2";

        public List<ExitSurveyAttribute> PersonalCircumstancesAttributes { get; set; }
        public List<ExitSurveyAttribute> EmployerAttributes { get; set; }
        public List<ExitSurveyAttribute> TrainingProviderAttributes { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Select what was the main reason for leaving your apprenticeship")]
        public int? SelectedAttributeId { get; set; }

        public PrimaryReasonModel(IExitSurveySessionService sessionService
                    , IApprenticeFeedbackService apprenticeFeedbackService)
                    : base(sessionService, apprenticeFeedbackService)
        {
        }

        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user)
        {
            LoadSessionData();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadSessionData();
                return Page();
            }

            ExitSurveyContext.PrimaryReason = SelectedAttributeId;
            SaveContext();

            if (ExitSurveyContext.CheckingAnswers)
            {
                return RedirectToPage("./checkyouranswers");
            }
            return RedirectToPage("./question3");
        }

        private void LoadSessionData()
        {
            PersonalCircumstancesAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.PersonalCircumstances).OrderBy(a => a.Ordering).ToList();
            EmployerAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.Employer).OrderBy(a => a.Ordering).ToList();
            TrainingProviderAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.TrainingProvider).OrderBy(a => a.Ordering).ToList();

            SelectedAttributeId = ExitSurveyContext.PrimaryReason;
        }
    }
}
