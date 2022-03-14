using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.ApprenticeFeedback.Domain.Configuration;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public static class ConfigurationStartup
    {
        //public static IWebHostBuilder ConfigureAzureTableConfiguration(this IWebHostBuilder hostBuilder)
        //{
        //    //hostBuilder.ConfigureAppConfiguration((hostingContext, configBuilder) =>
        //    //{

        //    //    configBuilder.AddAzureTableStorage(options =>
        //    //    {
        //    //        //var (names, connectionString, environment) = configBuilder.EmployerConfiguration();
        //    //        options.ConfigurationKeys = config["ConfigNames"].Split(",");
        //    //        options.StorageConnectionString = connectionString;
        //    //        options.EnvironmentName = environment;
        //    //        options.PreFixConfigurationKeys = false;
        //    //    });
        //    //});

        //    //return hostBuilder;
        //}
    }
}
