using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class FeedbackResult
    {
        public long Ukprn { get; set; }
        public int LarsCode { get; set; }
        public string ProviderName { get; set; }
        public DateTime DateTimeCompleted { get; set; }
    }
}
