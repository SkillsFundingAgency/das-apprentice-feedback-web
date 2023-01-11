using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeFeedback.Domain.UnitTests.Models
{
    public class WhenMappingFromApiResponseToDomainModel
    {
        [Test, MoqAutoData]
        public void Then_The_Attributes_Are_Correctly_Mapped(FeedbackAttribute source)
        {
            var result = (FeedbackAttribute)source;

            result.Id.Should().Be(source.Id);
            result.Name.Should().Be(source.Name);
        }
    }
}
