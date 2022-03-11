using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class TrainingProvider
    {
        public string Name { get; set; }
        public long Ukprn { get; set; }
        public IEnumerable<Apprenticeship> Apprenticeships { get; set; }

        private Apprenticeship _mostRecentApprenticeship => Apprenticeships.OrderByDescending(apprenticeship => apprenticeship.StartDate).FirstOrDefault();

        public DateTime GetEarliestFeedbackDate()
        {
            return _mostRecentApprenticeship.StartDate.AddMonths(3);
        }

        public DateTime? GetLatestSubmittedDate()
        {
            var latestApprenticeship = Apprenticeships.OrderByDescending(apprenticeship => apprenticeship.MostRecentFeedbackCompletion).FirstOrDefault();

            return latestApprenticeship.MostRecentFeedbackCompletion;
        }

        public int GetMostRecentLarsCode()
        {
            return _mostRecentApprenticeship.LarsCode;
        }

        public bool IsLessThanThreeMonthsSinceStartDate => _mostRecentApprenticeship.IsTooEarlyForFeedback;
        public bool IsMoreThanThreeMonthsSincePassing => _mostRecentApprenticeship.IsMoreThanThreeMonthsSincePassing;
        public bool IsMoreThanThreeMonthsSinceWithdrawal => _mostRecentApprenticeship.IsMoreThanThreeMonthsSinceWithdrawal;
        public bool HasRecentlyCompleted => _mostRecentApprenticeship.HasRecentlyCompletedFeedback;
        public bool HasGivenFinalFeedback => _mostRecentApprenticeship.HasGivenFinalFeedback;

        public bool IsValidForFeedback => !IsLessThanThreeMonthsSinceStartDate &&
                                            !IsMoreThanThreeMonthsSincePassing &&
                                            !IsMoreThanThreeMonthsSinceWithdrawal &&
                                            !HasRecentlyCompleted &&
                                            !HasGivenFinalFeedback;
    }
}
