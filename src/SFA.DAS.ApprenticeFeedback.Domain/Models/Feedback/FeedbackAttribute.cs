using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class FeedbackAttribute
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Required(ErrorMessage = "Select Agree or Disagree")]
        public FeedbackAttributeStatus? Status { get; set; }
    }
}
