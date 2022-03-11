using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;

namespace SFA.DAS.ApprenticeFeedback.Domain.UnitTests.Models
{
    public class ApprenticeshipTests
    {
        private Fixture _autoFixture;

        [SetUp]
        public void Arrange()
        {
            _autoFixture = new Fixture();
        }

        [Test]
        public void When_StartDateIsLessThenThreeMonthsAgo_Then_IsTooEarlyForFeedbackReturnsTrue()
        {
            var apprenticeship = _autoFixture.Build<Apprenticeship>()
                .With(a => a.StartDate, DateTime.UtcNow.AddMonths(-2))
                .Without(a => a.EndDate)
                .Without(a => a.FeedbackCompletionDates)
                .Create();

            apprenticeship.IsTooEarlyForFeedback.Should().BeTrue();
        }

        [Test]
        public void When_StartDateIsMoreThenThreeMonthsAgo_Then_IsTooEarlyForFeedbackReturnsFalse()
        {
            var apprenticeship = _autoFixture.Build<Apprenticeship>()
                .With(a => a.StartDate, DateTime.UtcNow.AddMonths(-4))
                .Without(a => a.EndDate)
                .Without(a => a.FeedbackCompletionDates)
                .Create();

            apprenticeship.IsTooEarlyForFeedback.Should().BeFalse();
        }
    }
}
