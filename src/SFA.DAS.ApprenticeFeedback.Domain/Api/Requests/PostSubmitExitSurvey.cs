using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Requests
{
    public class PostSubmitExitSurvey
    {
        public Guid ApprenticeFeedbackTargetId { get; set; }
        public bool AllowContact { get; set; }
        public bool DidNotCompleteApprenticeship { get; set; }
        public List<int> AttributeIds { get; set; }
        public int PrimaryReason { get; set; }
    }
}