using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback
{
    public class TrainingProvider
    {
        public string Name { get; set; }
        public long Ukprn { get; set; }
        public IEnumerable<Apprenticeship> Apprenticeships { get; set; }

        public Apprenticeship GetMostRecentlyStartedApprenticeship()
        {
            return Apprenticeships?.OrderByDescending(apprenticeship => apprenticeship.StartDate).FirstOrDefault();
        }
    }
}
