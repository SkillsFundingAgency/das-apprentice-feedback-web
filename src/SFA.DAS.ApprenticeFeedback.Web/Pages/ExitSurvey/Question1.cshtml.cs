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
    public class Question1Model : ExitSurveyContextPageModel, IHasBackLink
    {
        public string Backlink => (ExitSurveyContext.CheckingAnswers) ? $"./checkyouranswers" : $"./start/{ExitSurveyContext.ApprenticeFeedbackTargetId}";
        private readonly string Category = ExitSurveyAttributeCategory.ApprenticeshipStatus;

        // List of Ids of attributes in category "ApprenticeshipStatus" which
        // represent options where the apprentice HAS NOT withdrawn
        // These are set up in the AddDefaultExitSurveyAttribues.sql post-deployment script in the API project.
        private readonly List<int> NotWithdrawnIds = new List<int>() { 13, 14, 15, 16 };
        // List of Ids of attributes in category "ApprenticeshipStatus" which
        // represent options where the apprentice HAS withdrawn
        // These are set up in the AddDefaultExitSurveyAttribues.sql post-deployment script in the API project.
        private readonly List<int> WithdrawnIds = new List<int>() { 17 };

        [BindProperty]
        [Required(ErrorMessage = "Select where you are with your apprenticeship")]
        public int? SelectedAttributeId { get; set; }
        [BindProperty]
        public List<FeedbackAttribute> Attributes { get; set; }


        public Question1Model(IExitSurveySessionService sessionService,
            IApprenticeFeedbackService apprenticeFeedbackService)
            :base(sessionService, apprenticeFeedbackService)
        {
        }

        public async Task<IActionResult> OnGet([FromServices] AuthenticatedUser user)
        {
            // Get the questions (attributes) for this page.
            Attributes = new List<FeedbackAttribute>(await ApprenticeFeedbackService.GetExitSurveyAttributes(Category));

            // Get the selected attribute from the session if there is one.
            var selectedAttributes = ExitSurveyContext.Attributes.Where(a => a.Category == Category).ToList();
            if(null != selectedAttributes && selectedAttributes.Count == 1)
            {
                SelectedAttributeId = selectedAttributes[0].Id;
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // Validate

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save the selected attribute

            var selectedAttribute = Attributes.FirstOrDefault(a => a.Id == SelectedAttributeId.Value);
            if(null == selectedAttribute)
            {
                return Page();
            }

            // Are we on the check your answers journey and has the selection changed such that
            // the user would be on a different journey?
            if (ExitSurveyContext.CheckingAnswers)
            {
                if ((WithdrawnIds.Contains(selectedAttribute.Id) && !ExitSurveyContext.DidNotCompleteApprenticeship.Value)
                    ||
                    (NotWithdrawnIds.Contains(selectedAttribute.Id) && ExitSurveyContext.DidNotCompleteApprenticeship.Value)
                    )
                {
                    // Clear all the session data - effectively cancelling the check your answers journey
                    ExitSurveyContext.Reset();
                }
            }

            ExitSurveyContext.Clear(Category);
            ExitSurveyContext.Attributes.Add(selectedAttribute);
            SaveContext();

            // If the apprentice has withdrawn then continue with the survey
            if(WithdrawnIds.Contains(selectedAttribute.Id))
            {
                ExitSurveyContext.DidNotCompleteApprenticeship = true;
                SaveContext();
                return RedirectToPage("./question2");
            }
            // If the apprentice has not withdrawn then jump to confirmation page
            if (NotWithdrawnIds.Contains(selectedAttribute.Id))
            {
                ExitSurveyContext.DidNotCompleteApprenticeship = false;
                ExitSurveyContext.PrimaryReason = selectedAttribute.Id; // TBC: do we set the primary reason if they have not withdrawn?
                SaveContext();
                return RedirectToPage("./checkyouranswers");
            }

            // If we get to here, the lists of hard-coded IDs at the top of this class have
            // somehow gotten out of sync with the values in the Attribute table in the database.
            // This is bad.
            return RedirectToPage("/error");
        }
    }
}