using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey
{
    public class ExitSurveyAttribute : SurveyAttribute
    {
        public bool Value { get; set; }  // value for checkboxes

        public static ExitSurveyAttribute Create(FeedbackAttribute source)
        {
            return new ExitSurveyAttribute
            {
                Id = source.Id,
                Name = source.Name,
                Category = source.Category,
                Ordering = source.Ordering
            };
        }
    }
}
