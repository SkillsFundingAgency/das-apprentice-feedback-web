using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using SFA.DAS.ApprenticeFeedback.Web.Configuration;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Validators
{
    /*
    public class ApprenticeshipFeedbackEligibilityValidatorTests
    {
        private static readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        static ApprenticeshipFeedbackEligibilityValidatorTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _mockDateTimeProvider.SetupGet(m => m.UtcNow).Returns(DateTime.UtcNow);
        }

        public class IsInInitialFallowPeriod
        {
            private readonly ApprenticeshipFeedbackEligibilityValidator _validatorUnderTest;

            public IsInInitialFallowPeriod()
            {
                var config = new FeedbackConfiguration()
                {
                    InitialFallowPeriodDays = 90,
                };
                _validatorUnderTest = new ApprenticeshipFeedbackEligibilityValidator(_mockDateTimeProvider.Object, config);
            }

            private static IEnumerable<TestCaseData> InitialFallowPeriodTestCases()
            {
                yield return new TestCaseData(new Apprenticeship() { StartDate = _mockDateTimeProvider.Object.UtcNow.AddDays(-89) }, true);
                yield return new TestCaseData(new Apprenticeship() { StartDate = _mockDateTimeProvider.Object.UtcNow.AddDays(-90) }, false);
                yield return new TestCaseData(new Apprenticeship() { StartDate = _mockDateTimeProvider.Object.UtcNow.AddDays(-91) }, false);
            }

            [Test, TestCaseSource(nameof(InitialFallowPeriodTestCases))]
            public void True_When_Apprenticeship_Start_Date_Plus_FallowPeriod_Is_After_Now(Apprenticeship apprenticeship, bool expectedResult)
            {
                var validationResult = _validatorUnderTest.IsInInitialFallowPeriod(apprenticeship);
                validationResult.Should().Be(expectedResult);
            }
        }

        public class IsInPostFeedbackFallowPeriod
        {
            private readonly ApprenticeshipFeedbackEligibilityValidator _validatorUnderTest;

            public IsInPostFeedbackFallowPeriod()
            {
                var config = new FeedbackConfiguration()
                {
                    PostFeedbackFallowPeriodDays = 21
                };
                _validatorUnderTest = new ApprenticeshipFeedbackEligibilityValidator(_mockDateTimeProvider.Object, config);
            }

            private static IEnumerable<TestCaseData> PostFeedbackFallowPeriodTestCases()
            {
                yield return new TestCaseData(new Apprenticeship() {  }, false).SetDescription("No feedback completion ");
                yield return new TestCaseData(new Apprenticeship() { FeedbackCompletionDates = new DateTime[] { _mockDateTimeProvider.Object.UtcNow.AddDays(-20) } }, true);
                yield return new TestCaseData(new Apprenticeship() { FeedbackCompletionDates = new DateTime[] { _mockDateTimeProvider.Object.UtcNow.AddDays(-21) } }, true);
            }

            [Test, TestCaseSource(nameof(PostFeedbackFallowPeriodTestCases))]
            public void True_When_Apprenticeship_End_Date_Plus_FallowPeriod_Is_After_Now(Apprenticeship apprenticeship, bool expectedResult)
            {
                var validationResult = _validatorUnderTest.IsInPostFeedbackFallowPeriod(apprenticeship);
                validationResult.Should().Be(expectedResult);
            }

        }
    }
    */
}
