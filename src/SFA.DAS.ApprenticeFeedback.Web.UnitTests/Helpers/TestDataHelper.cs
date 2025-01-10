using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Helpers
{
    public static class TestDataHelper
    {
        public static FeedbackContext CreateFeedbackContextWithAttributes(Guid apprenticeId)
        {
            var attributes = CreateFeedbackAttributes();

            var feedbackContext = new FeedbackContext
            {
                ProviderName = "Test Provider",
                UkPrn = 10000034,
                ProviderCount = 1,
                ApprenticeFeedbackTargetId = apprenticeId,
                FeedbackAttributes = attributes,
                FeedbackEligibility = FeedbackEligibility.Allow,
                LarsCode = 2,
                OverallRating = OverallRating.VeryPoor,
            };

            return feedbackContext;
        }

        public static List<FeedbackSurveyAttribute> CreateFeedbackAttributes()
        {
            var attributes = new List<FeedbackSurveyAttribute>();

            var attribute1 = new FeedbackSurveyAttribute
            {
                Id = 1,
                Name = "TestAttribute1",
                Category = "Organization",
                Status = FeedbackAttributeStatus.Disagree
            };

            var attribute2 = new FeedbackSurveyAttribute
            {
                Id = 2,
                Name = "TestAttribute2",
                Category = "Organization",
                Status = FeedbackAttributeStatus.Agree
            };

            var attribute3 = new FeedbackSurveyAttribute
            {
                Id = 3,
                Name = "TestAttribute3",
                Category = "Organization",
                Status = FeedbackAttributeStatus.Agree
            };

            return attributes;
        }

        public static TrainingProvider CreateTrainingProvider(int ukprn)
        {
            var provider = new TrainingProvider
            {
                Ukprn = ukprn,
                ApprenticeFeedbackTargetId = Guid.NewGuid(),
                Name = "The Test Provider",
                LarsCode = 33,
                FeedbackEligibility = FeedbackEligibility.Allow
            };

            return provider;
        }
    }
}
