using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey
{
    public enum UserJourney
    {
        Start,
        DidNotComplete,
        DidComplete,
        Finished
    }

    public class ExitSurveyContext
    {
        public UserJourney UserJourney { get; set; }  // helps us police which pages can be accessed

        public Guid ApprenticeFeedbackTargetId { get; set; }

        public bool CheckingAnswers { get; set; }

        // Question 1 - did you NOT complete the apprenticeship?
        public bool? DidNotCompleteApprenticeship { get; set; }

        // Happy path - no I did not complete the apprenticeship
        public string IncompletionReason { get; set; }
        public bool IncompletionFactor_Caring { get; set; }
        public bool IncompletionFactor_Family { get; set; }
        public bool IncompletionFactor_Financial { get; set; }
        public bool IncompletionFactor_Mental { get; set; }
        public bool IncompletionFactor_Physical { get; set; }
        public bool IncompletionFactor_None { get; set; }

        public string RemainedReason { get; set; }

        // Unhappy path - yes I did complete the apprenticeship, your information about me is incorrect
        public string ReasonForIncorrect { get; set; }

        // Timestamp of the previously completed exit survey if any
        public DateTime? DateTimeCompleted { get; set; }

        // Common to happy and unhappy paths
        public bool AllowContact { get; set; }


        public void Reset()
        {
            //UserJourney = UserJourney.Common;
            CheckingAnswers = false;
            DidNotCompleteApprenticeship = null;
            IncompletionReason = null;
            IncompletionFactor_Caring = false;
            IncompletionFactor_Family = false;
            IncompletionFactor_Financial = false;
            IncompletionFactor_Mental = false;
            IncompletionFactor_None = false;
            IncompletionFactor_Physical = false;
            RemainedReason = null;
            ReasonForIncorrect = null;
            AllowContact = false;
        }
    }
}
