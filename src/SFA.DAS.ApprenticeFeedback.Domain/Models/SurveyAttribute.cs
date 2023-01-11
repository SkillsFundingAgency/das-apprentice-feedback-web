namespace SFA.DAS.ApprenticeFeedback.Domain.Models
{
    public abstract class SurveyAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Ordering { get; set; }
    }
}
