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
using System.Linq;
using System.Threading.Tasks;


namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitSurvey
{
    [HideNavigationBar]
    public class Question2Model : ExitSurveyContextPageModel, IHasBackLink
    {
        public string Backlink => (ExitSurveyContext.CheckingAnswers)? $"./checkyouranswers" : $"./question1";

        [BindProperty]
        public List<FeedbackAttribute> PersonalCircumstancesAttributes { get; set; }
        [BindProperty]
        public List<FeedbackAttribute> EmployerAttributes { get; set; }
        [BindProperty]
        public List<FeedbackAttribute> TrainingProviderAttributes { get; set; }

        public Question2Model(IExitSurveySessionService sessionService
            , IApprenticeFeedbackService apprenticeFeedbackService)
            : base(sessionService, apprenticeFeedbackService)
        {
        }

        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user)
        {
            // Get the questions (attributes) for this page.
            PersonalCircumstancesAttributes = new List<FeedbackAttribute>(await ApprenticeFeedbackService.GetExitSurveyAttributes(ExitSurveyAttributeCategory.PersonalCircumstances));
            EmployerAttributes = new List<FeedbackAttribute>(await ApprenticeFeedbackService.GetExitSurveyAttributes(ExitSurveyAttributeCategory.Employer));
            TrainingProviderAttributes = new List<FeedbackAttribute>(await ApprenticeFeedbackService.GetExitSurveyAttributes(ExitSurveyAttributeCategory.TrainingProvider));

            // Load session state if it exists

            var selectedPersonalCircumstancesAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.PersonalCircumstances);
            foreach(var sessionAttribute in selectedPersonalCircumstancesAttributes)
            {
                var a = PersonalCircumstancesAttributes.Find(a => a.Id == sessionAttribute.Id);
                a.Value = true;
            }

            var selectedEmployerAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.Employer);
            foreach (var sessionAttribute in selectedEmployerAttributes)
            {
                var a = EmployerAttributes.Find(a => a.Id == sessionAttribute.Id);
                a.Value = true;
            }

            var selectedTrainingProviderAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.TrainingProvider);
            foreach (var sessionAttribute in selectedTrainingProviderAttributes)
            {
                var a = TrainingProviderAttributes.Find(a => a.Id == sessionAttribute.Id);
                a.Value = true;
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            // Make sure something is selected

            var selectedPersonalCircumstancesAttributes = PersonalCircumstancesAttributes.Where(a => a.Value).ToList();
            var selectedEmployerAttributes = EmployerAttributes.Where(a => a.Value).ToList();
            var selectedTrainingProviderAttributes = TrainingProviderAttributes.Where(a => a.Value).ToList();

            int selectedCount = selectedPersonalCircumstancesAttributes.Count()
                + selectedEmployerAttributes.Count()
                + selectedTrainingProviderAttributes.Count();

            if (0 == selectedCount)
            {
                ModelState.AddModelError("MultipleErrorSummary", "Select the factors that contributed to you not finishing your apprenticeship");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Decide if something has changed
            var changedAnswers = false;
            if (ExitSurveyContext.Attributes.Any(a => a.Category == ExitSurveyAttributeCategory.PersonalCircumstances)
                || selectedPersonalCircumstancesAttributes.Any())
            {
                var selected = selectedPersonalCircumstancesAttributes.Select(a => a.Id).Except(ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.PersonalCircumstances).Select(a => a.Id));
                var deselected = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.PersonalCircumstances).Select(a => a.Id).Except(selectedPersonalCircumstancesAttributes.Select(a => a.Id));
                changedAnswers |= (selected.Any() || deselected.Any());
            }
            if (ExitSurveyContext.Attributes.Any(a => a.Category == ExitSurveyAttributeCategory.Employer)
                || selectedEmployerAttributes.Any())
            {
                var selected = selectedEmployerAttributes.Select(a => a.Id).Except(ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.Employer).Select(a => a.Id));
                var deselected = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.Employer).Select(a => a.Id).Except(selectedEmployerAttributes.Select(a => a.Id));
                changedAnswers |= (selected.Any() || deselected.Any());
            }
            if (ExitSurveyContext.Attributes.Any(a => a.Category == ExitSurveyAttributeCategory.TrainingProvider)
                || selectedTrainingProviderAttributes.Any())
            {
                var selected = selectedTrainingProviderAttributes.Select(a => a.Id).Except(ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.TrainingProvider).Select(a => a.Id));
                var deselected = ExitSurveyContext.Attributes.Where(a => a.Category == ExitSurveyAttributeCategory.TrainingProvider).Select(a => a.Id).Except(selectedTrainingProviderAttributes.Select(a => a.Id));
                changedAnswers |= (selected.Any() || deselected.Any());
            }

            // Reset any previously selected attributes in the session
            ExitSurveyContext.Clear(ExitSurveyAttributeCategory.PersonalCircumstances);
            ExitSurveyContext.Clear(ExitSurveyAttributeCategory.Employer);
            ExitSurveyContext.Clear(ExitSurveyAttributeCategory.TrainingProvider);
            // Add the selected attributes to the session
            foreach (var a in selectedPersonalCircumstancesAttributes)
            {
                ExitSurveyContext.Attributes.Add(a);
            }
            foreach (var a in selectedEmployerAttributes)
            {
                ExitSurveyContext.Attributes.Add(a);
            }
            foreach (var a in selectedTrainingProviderAttributes)
            {
                ExitSurveyContext.Attributes.Add(a);
            }
            SaveContext();

            FeedbackAttribute singularAttributeSelection = null;
            if (1 == selectedCount)
            {
                if (selectedPersonalCircumstancesAttributes.Any())
                {
                    singularAttributeSelection = selectedPersonalCircumstancesAttributes[0];
                }
                else if (selectedEmployerAttributes.Any())
                {
                    singularAttributeSelection = selectedEmployerAttributes[0];
                }
                else if (selectedTrainingProviderAttributes.Any())
                {
                    singularAttributeSelection = selectedTrainingProviderAttributes[0];
                }
            }

            if (ExitSurveyContext.CheckingAnswers)
            {
                string pageRedirect = "./checkyouranswers";
                if (changedAnswers)
                {
                    if (singularAttributeSelection != null)
                    {
                        ExitSurveyContext.PrimaryReason = singularAttributeSelection.Id;
                    }
                    else
                    {
                        ExitSurveyContext.PrimaryReason = null;
                        pageRedirect = "./primaryreason";
                    }
                    SaveContext();
                }
                //If changed answers, go to check answers if only one selection, as this auto selects primary reason
                // or continue to primary reason to be selected if more than one option remains
                return RedirectToPage(pageRedirect);
            }
            else
            {
                // If there is only one attribute selected then
                // set it as the primary reason and fast-forward to check your answers
                if (null != singularAttributeSelection)
                {
                    ExitSurveyContext.PrimaryReason = singularAttributeSelection.Id;
                    SaveContext();
                    return RedirectToPage("./question3");
                }
                
                return RedirectToPage("./primaryreason");
            }
        }
    }
}