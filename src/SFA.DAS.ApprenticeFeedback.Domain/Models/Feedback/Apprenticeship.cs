using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class Apprenticeship
    {
        public int LarsCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; }

        // @ToDo: we are assuming these are in date order, latest first. Fair assumption?
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

        public DateTime? GetMostRecentFeedbackCompletionDate() => FeedbackCompletionDates?.OrderByDescending(d => d).FirstOrDefault();
    }
}
