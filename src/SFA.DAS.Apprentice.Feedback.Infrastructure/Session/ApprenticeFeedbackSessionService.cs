using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public class ApprenticeFeedbackSessionService : IApprenticeFeedbackSessionService
    {
        private ISessionService _sessionService;

        private const string _sessionKey = "Apprentice_Feedback_Request";

        public ApprenticeFeedbackSessionService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public void StartNewFeedbackRequest(string provderName, long ukprn, int larsCode)
        {
            var request = new FeedbackRequest
            {
                TrainingProvider = provderName,
                Ukprn = ukprn,
                LarsCode = larsCode
            };

            _sessionService.Set(_sessionKey, request);
        }

        public FeedbackRequest GetFeedbackRequest()
        {
            return _sessionService.Get<FeedbackRequest>(_sessionKey);
        }

        public void UpdateFeedbackRequest(FeedbackRequest request)
        {
            _sessionService.Set(_sessionKey, request);
        }

    }
}
