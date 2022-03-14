using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.ApprenticeFeedback.Domain.Configuration;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public static class ConfigurationStartup
    {
        public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<FindApprenticeshipTrainingConfiguration>(configuration.GetSection("FindApprenticeshipTraining"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<FindApprenticeshipTrainingConfiguration>>().Value);
        }
    }
}
