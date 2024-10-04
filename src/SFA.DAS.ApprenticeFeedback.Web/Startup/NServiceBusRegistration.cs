using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using SFA.DAS.ApprenticeFeedback.Application.Settings;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    [ExcludeFromCodeCoverage]
    public static class NServiceBusRegistration
    {
        public static IServiceCollection AddNServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            var endpointConfiguration = new EndpointConfiguration("SFA.DAS.ApprenticeFeedback.Web");

            if (appSettings.NServiceBusConnectionString.Equals("UseLearningEndpoint=true", StringComparison.CurrentCultureIgnoreCase))
            {
                endpointConfiguration.UseTransport<LearningTransport>();
            }
            else
            {
                var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
                transport.ConnectionString(appSettings.NServiceBusConnectionString);
            }

            if (!string.IsNullOrEmpty(appSettings.NServiceBusLicense))
            {
                endpointConfiguration.License(appSettings.NServiceBusLicense);
            }

            endpointConfiguration.SendOnly();
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            endpointConfiguration.Conventions()
                .DefiningCommandsAs(t => Regex.IsMatch(t.Name, "Command(V\\d+)?$"))
                .DefiningEventsAs(t => Regex.IsMatch(t.Name, "Event(V\\d+)?$"));

            var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            services.AddSingleton(endpointInstance);
            services.AddSingleton<IMessageSession>(endpointInstance);

            return services;
        }
    }
}
