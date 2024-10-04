using NUnit.Framework;
using FluentAssertions;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;

namespace SFA.DAS.ApprenticeFeedback.Domain.UnitTests.Models
{
    public class WhenMappingFromApiResponseToDomainModel
    {
        [Test]
        public void Then_The_Attributes_Are_Correctly_Mapped()
        {
            // Arrange
            var source = new FeedbackAttribute
            {
                Id = 1,
                Name = "Test Feedback Attribute"
            };

            // Act
            var result = (FeedbackAttribute)source;

            // Assert
            result.Id.Should().Be(source.Id);
            result.Name.Should().Be(source.Name);
        }
    }
}
