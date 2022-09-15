using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Requests
{
    public class PostSubmitExitSurvey
    {
        public Guid ApprenticeFeedbackTargetId { get; set; }
        public bool DidNotCompleteApprenticeship { get; set; }
        public string IncompletionReason { get; set; }
        public string IncompletionFactor { get; set; }
        public string RemainedReason { get; set; }
        public string ReasonForIncorrect { get; set; }
        public bool AllowContact { get; set; }
    }
}
