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

        public void StartNewFeedbackRequest()
        {
            var request = new FeedbackRequest();

            // temporary feedback attributes
            request.FeedbackAttributes = new List<FeedbackAttribute>
            {
                new FeedbackAttribute { Title = "Organising well-structured training" },
                new FeedbackAttribute { Title = "Communicating clearly with you" },
                new FeedbackAttribute { Title = "Providing accessible training resources" }
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
