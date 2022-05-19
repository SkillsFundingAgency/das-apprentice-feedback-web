using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Extensions;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Domain.UnitTests.Extensions
{
    public class IEnumerableExtensionsTests
    {
        public class ContainsCountItems
        {
            [Test]
            [TestCase(new string[] { "A", "B", "C", "D", "E" }, 5, true)]
            [TestCase(new string[] { "A", "B", "C", "D", "E" }, 4, false)]
            [TestCase(new string[] { "A", "B", "C", "D", "E" }, 0, false)]
            [TestCase(new string[] { }, 0, true)]
            public void When_Count_Is_Not_Negative_Then_Count_Is_Evaluated(IEnumerable<string> items, int count, bool expectedResult)
            {
                var result = items.ContainsCountItems(count);

                result.Should().Be(expectedResult);
            }

            [Test]
            public void When_Count_Is_Negative_Then_Throws_ArgumentException()
            {
                Action act = () => new string[] { "A", "B", "C", "D", "E" }.ContainsCountItems(-1);

                act.Should().Throw<ArgumentException>().WithMessage("Count must be greater than or equal to zero*");
            }

            [Test]
            public void When_Enumeration_Is_Null_Then_Throws_ArgumentException()
            {
                IEnumerable<string> items = null;
                Action act = () => items.ContainsCountItems(-1);

                act.Should().Throw<ArgumentException>().WithMessage("Enumeration cannot be null*");
            }
        }
    }
}
