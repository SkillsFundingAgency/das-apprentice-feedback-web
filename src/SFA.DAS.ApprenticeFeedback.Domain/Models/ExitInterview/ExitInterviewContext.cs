using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.ExitInterview
{
    public class ExitInterviewContext
    {
        public Guid ApprenticeFeedbackTargetId { get; set; }

        public bool CheckingAnswers { get; set; }

        // Question 1 - did you NOT complete the apprenticeship?
        public bool? DidNotCompleteApprenticeship { get; set; }

        // Happy path - no I did not complete the apprenticeship
        public string IncompletionReason { get; set; }
        public string IncompletionFactor { get; set; }
        public string RemainedReason { get; set; }

        // Unhappy path - yes I did complete the apprenticeship, your information about me is incorrect
        public string ReasonForIncorrect { get; set; }

        // Common to happy and unhappy paths
        public bool AllowContact { get; set; }


        public void Reset()
        {
            CheckingAnswers = false;
            DidNotCompleteApprenticeship = null;
            IncompletionReason = null;
            IncompletionFactor = null;
            RemainedReason = null;
            ReasonForIncorrect = null;
            AllowContact = false;
        }
    }
}
