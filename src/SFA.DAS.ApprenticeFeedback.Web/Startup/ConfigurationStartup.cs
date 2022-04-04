using Microsoft.AspNetCore.Hosting;
using SFA.DAS.Configuration.AzureTableStorage;

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
