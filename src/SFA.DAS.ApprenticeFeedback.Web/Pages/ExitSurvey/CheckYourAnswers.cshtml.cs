using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Requests;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class CheckYourAnswersModel : ExitSurveyContextPageModel, IHasBackLink
    {
        [BindProperty]
        public bool AllowContact { get; set; }

        public string Backlink => (ExitSurveyContext.DidNotCompleteApprenticeship.Value) ? $"./question4" : $"./question1";

        [BindProperty]
        public List<ExitSurveyAttribute> PersonalCircumstancesAttributes { get; set; }
        [BindProperty]
        public List<ExitSurveyAttribute> EmployerAttributes { get; set; }
        [BindProperty]
        public List<ExitSurveyAttribute> TrainingProviderAttributes { get; set; }

        [BindProperty]
        public List<ExitSurveyAttribute> RemainFactorAttributes { get; set; }

        private readonly IDateTimeProvider _dateTimeProvider;

        public CheckYourAnswersModel(IExitSurveySessionService sessionService, 
            IApprenticeFeedbackService apprenticeFeedbackService,
            IDateTimeProvider dateTimeProvider)
            : base(sessionService, apprenticeFeedbackService)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public IActionResult OnGet([FromServices] AuthenticatedUser user)
        {
            ExitSurveyContext.CheckingAnswers = true;
            
            PersonalCircumstancesAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.PersonalCircumstances).ToList();
            EmployerAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.Employer).ToList();
            TrainingProviderAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.TrainingProvider).ToList();
            RemainFactorAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.RemainFactors).ToList();

            SaveContext();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitSurveyContext.AllowContact = AllowContact;

            // Save 
            var request = new PostSubmitExitSurvey
            {
                ApprenticeFeedbackTargetId = ExitSurveyContext.ApprenticeFeedbackTargetId.Value,
                AllowContact = ExitSurveyContext.AllowContact.Value,
                DidNotCompleteApprenticeship = ExitSurveyContext.DidNotCompleteApprenticeship.Value,
                AttributeIds = ExitSurveyContext.Attributes.Select(a => a.Id).ToList(),
                PrimaryReason = ExitSurveyContext.PrimaryReason.Value,
            };
            await ApprenticeFeedbackService.SubmitExitSurvey(request);

            // Prevent a resubmit.
            ExitSurveyContext.SurveyCompleted = true;
            ExitSurveyContext.CheckingAnswers = false;
            SaveContext();

            if(ExitSurveyContext.DidNotCompleteApprenticeship.Value)
            {
                return RedirectToPage("./complete");
            }

            return RedirectToPage("./incorrectcomplete");
        }
    }
}