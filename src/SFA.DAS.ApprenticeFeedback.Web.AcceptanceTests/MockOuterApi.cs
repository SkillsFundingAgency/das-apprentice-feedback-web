using System;
using System.Collections.Generic;
using System.Text;
using WireMock.Server;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests
{
    public class MockOuterApi : IDisposable
    {
        private bool _isDisposed;

        public string BaseAddress { get; private set; }

        public WireMockServer MockServer { get; private set; }

        public MockOuterApi()
        {
            MockServer = WireMockServer.Start();
            BaseAddress = MockServer.Urls[0];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Reset()
        {
            MockServer.Reset();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
            {
                if (MockServer.IsStarted)
                {
                    MockServer.Stop();
                }
                MockServer.Dispose();
            }

            _isDisposed = true;
        }

        internal string UrlPath(string path) => $"{MockServer.Urls[0]}/{path}";
    }
}
