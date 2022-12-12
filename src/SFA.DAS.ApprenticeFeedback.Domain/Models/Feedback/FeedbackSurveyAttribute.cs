using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class FeedbackSurveyAttribute : SurveyAttribute
    {
        public const string ErrorMessage = "Select Agree or Disagree";
        [Required(ErrorMessage = ErrorMessage)]
        public FeedbackAttributeStatus? Status { get; set; }  // value for radio buttons

        public static FeedbackSurveyAttribute Create(FeedbackAttribute source)
        {
            return new FeedbackSurveyAttribute
            {
                Id = source.Id,
                Name = source.Name,
                Category = source.Category,
                Ordering = source.Ordering
            };
        }
    }
}
