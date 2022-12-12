namespace SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey
{
    public class ExitSurveyAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Ordering { get; set; }
        public bool Value { get; set; }  // value for checkboxes

        public static implicit operator ExitSurveyAttribute(Api.Responses.FeedbackAttribute source)
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
