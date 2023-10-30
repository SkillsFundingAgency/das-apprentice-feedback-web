using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey
{
    public class ExitSurveyContext
    {
        // Exit Survey data payload
        public Guid? ApprenticeFeedbackTargetId { get; set; }
        public HashSet<ExitSurveyAttribute> Attributes { get; set; }
        public bool? AllowContact { get; set; }
        public bool? DidNotCompleteApprenticeship { get; set; }
        public int? PrimaryReason { get; set; }

        // State management
        public bool CheckingAnswers { get; set; }
        public bool? SurveyCompleted { get; set; }
        public DateTime? DateTimeCompleted { get; set; }

        public ExitSurveyContext()
        {
            Reset();
        }

        public void Reset()
        {
            Attributes = new HashSet<ExitSurveyAttribute>();
            CheckingAnswers = false;
            AllowContact = null;
            DidNotCompleteApprenticeship = null;
            PrimaryReason = null;
            SurveyCompleted = null;
            DateTimeCompleted = null;
        }

        public void Clear(string category)
        {
            Attributes.RemoveWhere(a => a.Category == category);
        }
    }
}
