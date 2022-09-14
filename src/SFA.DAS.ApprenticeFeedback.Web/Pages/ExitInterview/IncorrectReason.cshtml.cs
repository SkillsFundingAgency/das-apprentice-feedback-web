using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Web.Pages.ExitInterview
{
    [HideNavigationBar]
    public class IncorrectReasonModel : ExitInterviewContextPageModel, IHasBackLink
    {
        [BindProperty]
        [Required(ErrorMessage = "Select a reason")]
        public string ReasonForIncorrect { get; set; }
        public string[] Reasons = new[]
        {
            "I am still doing my apprenticeship",
            "I completed the apprenticeship and had my end point assessment",
            "I completed my training and I'm waiting for the end point assessment",
            "I never started an apprenticeship"
        };

        [BindProperty]
        public bool AllowContact { get; set; }

        public string Backlink => $"./question1";

        public IncorrectReasonModel(IExitInterviewSessionService sessionService)
            : base(sessionService)
        {
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExitInterviewContext.ReasonForIncorrect = ReasonForIncorrect;
            ExitInterviewContext.AllowContact = AllowContact;
            SaveContext();

            return RedirectToPage("./incorrectcomplete");
        }

    }
}
