using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.UnitTests.Session
{
    public class SessionServiceTests
    {
        private Mock<IHttpContextAccessor> _mockContextAccessor;
        private const string _environment = "test-env";

        private SessionService _sessionService;

        [SetUp]
        public void Arrange()
        {
            _mockContextAccessor = new Mock<IHttpContextAccessor>();

            _sessionService = new SessionService(_mockContextAccessor.Object, _environment);
        }

        // To do: Tests for Set, Get, Remove, update and exists
    }
}
