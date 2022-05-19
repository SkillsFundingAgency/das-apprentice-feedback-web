using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class FeedbackAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Select Agree or Disagree")]
        public FeedbackAttributeStatus? Status { get; set; }

        public static implicit operator FeedbackAttribute(ProviderAttribute source)
        {
            return new FeedbackAttribute
            { 
                Id = source.Id,
                Name = source.Name
            };
        }
    }
}
