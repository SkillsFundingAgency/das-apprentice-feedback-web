using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Pages
{
    //public class WhenRequestingProviderAttributes
    //{

    //    private Mock<IApprenticeFeedbackApi> _clientMock;
    //    private ApprenticeFeedbackService _service;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        _clientMock = new Mock<IApprenticeFeedbackApi>();
    //        _service = new ApprenticeFeedbackService(_clientMock.Object);
    //    }

    //    [Test]
    //    public async Task Then_Provider_Attributes_Are_Correctly_Returned()
    //    {
    //        // Arrange
    //        var response = new List<ProviderAttribute>
    //    {
    //        // Populate with test data
    //        new ProviderAttribute { /* Set properties */ },
    //        new ProviderAttribute { /* Set properties */ }
    //    };

    //        _clientMock.Setup(x => x.GetProviderAttributes()).ReturnsAsync(response);

    //        // Act
    //        var result = await _service.GetFeedbackAttributes();

    //        // Assert
    //        result.Should().BeEquivalentTo(response);
    //    }
    //}
}