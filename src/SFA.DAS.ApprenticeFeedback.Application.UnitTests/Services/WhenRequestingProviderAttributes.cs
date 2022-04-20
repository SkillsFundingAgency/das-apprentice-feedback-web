using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Api.Responses;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.UnitTests.Pages
{
    public class WhenRequestingProviderAttributes
    {
        /*
        [Test, MoqAutoData]
        public async Task Then_Provider_Attributes_Are_Correctly_Returned
            (
            [Frozen] Mock<IApprenticeFeedbackApi> client,
            [Greedy] ApprenticeFeedbackService service,
            List<ProviderAttribute> response
            )
        {
            client.Setup(x => x.GetProviderAttributes()).ReturnsAsync(response);

            var result = await service.GetFeedbackAttributes();
            
            result.Should().BeEquivalentTo(response);
        }
        */
    }
}
