using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class Apprenticeship
    {
        private DateTime _currentDate = DateTime.Now;

        public int LarsCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<DateTime> FeedbackCompletionDates { get; set; }

        public static implicit operator Apprenticeship(Api.Responses.Apprenticeship source)
        {
            return new Apprenticeship
            {
                LarsCode = source.LarsCode,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Status = source.Status,
                UpdatedAt = source.UpdatedAt,
                FeedbackCompletionDates = source.FeedbackCompletionDates
            };
        }

        public DateTime? MostRecentFeedbackCompletion => FeedbackCompletionDates.OrderByDescending(d => d).FirstOrDefault();

        public bool IsTooEarlyForFeedback => StartDate > _currentDate.AddMonths(-3);
        public bool HasGivenFinalFeedback => EndDate.HasValue && MostRecentFeedbackCompletion > EndDate.Value;
        public bool IsMoreThanThreeMonthsSincePassing => !HasGivenFinalFeedback && Status == "Pass" && _currentDate > EndDate.Value.AddMonths(3);
        public bool IsMoreThanThreeMonthsSinceWithdrawal => !HasGivenFinalFeedback && Status == "Withdrawn" && _currentDate > EndDate.Value.AddMonths(3);
        public bool HasRecentlyCompletedFeedback => !HasGivenFinalFeedback && MostRecentFeedbackCompletion > _currentDate.AddMonths(-3);

        public bool IsValidForFeedback => !IsTooEarlyForFeedback &&
                                            !IsMoreThanThreeMonthsSincePassing &&
                                            !IsMoreThanThreeMonthsSinceWithdrawal &&
                                            !HasGivenFinalFeedback &&
                                            !HasRecentlyCompletedFeedback;
    }
}
