using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Api.Responses
{
    public enum ExitSurveyStatus
    {
        Valid,
        Invalid_NotWithdrawn,
        Invalid_CompletedSurvey,
        Invalid_NotWithdrawnCompletedSurvey
    }

    public class ApprenticeFeedbackTargetStatus
    {
        public Guid ApprenticeFeedbackTargetId { get; set; }
        public ExitSurveyStatus ExitSurveyStatus { get; set; }
    }
}
