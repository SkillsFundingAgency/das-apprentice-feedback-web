using Microsoft.AspNetCore.Hosting;
using SFA.DAS.Configuration.AzureTableStorage;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public static class ConfigurationStartup
    {
        public static IWebHostBuilder ConfigureAzureTableConfiguration(this IWebHostBuilder hostBuilder)
        {
            //hostBuilder.ConfigureAppConfiguration((hostingContext, configBuilder) =>
            //{
            //    if (hostingContext.HostingEnvironment.IsDevelopment()) return;

            //    configBuilder.AddAzureTableStorage(options =>
            //    {
            //        var (names, connectionString, environment) = configBuilder.EmployerConfiguration();
            //        options.ConfigurationKeys = names.Split(",");
            //        options.StorageConnectionString = connectionString;
            //        options.EnvironmentName = environment;
            //        options.PreFixConfigurationKeys = false;
            //    });
            //});

            return hostBuilder;
        }
    }
}
