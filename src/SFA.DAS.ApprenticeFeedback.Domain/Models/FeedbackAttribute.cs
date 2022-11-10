namespace SFA.DAS.ApprenticeFeedback.Domain.Models
{
    public class FeedbackAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Ordering { get; set; }
        public FeedbackAttributeStatus? Status { get; set; }  // value for radio buttons
        public bool Value { get; set; }  // value for checkboxes

        public static implicit operator FeedbackAttribute(Api.Responses.FeedbackAttribute source)
        {
            return new FeedbackAttribute
            {
                Id = source.Id,
                Name = source.Name,
                Category = source.Category,
                Ordering = source.Ordering
            };
        }
    }
}
