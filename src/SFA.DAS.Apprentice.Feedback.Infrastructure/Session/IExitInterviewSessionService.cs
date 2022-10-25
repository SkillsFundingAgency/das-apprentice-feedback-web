using SFA.DAS.ApprenticeFeedback.Domain.Models.ExitSurvey;

namespace SFA.DAS.ApprenticeFeedback.Infrastructure.Session
{
    public interface IExitSurveySessionService
    {
        ExitSurveyContext GetExitSurveyContext();
        void SetExitSurveyContext(ExitSurveyContext ExitSurveyContext);
    }
}
