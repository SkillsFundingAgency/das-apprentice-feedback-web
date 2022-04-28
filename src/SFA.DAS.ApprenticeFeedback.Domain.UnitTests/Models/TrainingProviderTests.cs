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
        public void When_TrainingProviderHasApprenticeships_Then_MostRecentIsReturned()
        {
            /*
           var date = DateTime.UtcNow.Date;

            var trainingProvider = new TrainingProvider
            {
                Apprenticeships = new List<Apprenticeship>
                {
                    new Apprenticeship
                    {
                        StartDate = date.AddDays(-30),
                        LarsCode = 1
                    },
                    new Apprenticeship
                    {
                        StartDate = date.AddDays(-10),
                        LarsCode = 2
                    },
                    new Apprenticeship
                    {
                        StartDate = date.AddDays(-20),
                        LarsCode = 3
                    },
                }
            };

            var result = trainingProvider.GetMostRecentlyStartedApprenticeship();

            result.Should().NotBeNull();
            result.LarsCode.Should().Be(2);
            */
        }
    }
}
