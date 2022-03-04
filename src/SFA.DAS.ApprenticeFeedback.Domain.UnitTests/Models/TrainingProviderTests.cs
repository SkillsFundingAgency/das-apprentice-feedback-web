using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeFeedback.Domain.UnitTests.Models
{
    public class TrainingProviderTests
    {

        [Test]
        public void When_GettingLatestSubmissionDate_Then_CorrectDateIsReturned()
        {
            var date = DateTime.UtcNow.Date;

            var trainingProvider = new TrainingProvider
            {
                Apprenticeships = new List<Apprenticeship>
                {
                    new Apprenticeship
                    {
                        FeedbackCompletionDates = new List<DateTime>
                        {
                            date.AddDays(-2),
                            date
                        }
                    }
                }
            };

            var result = trainingProvider.LatestSubmittedDate.Value;

            result.Should().Be(date);
        }
    }
}
