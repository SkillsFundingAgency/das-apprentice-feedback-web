using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.ApprenticeFeedback.Web.Configuration;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticeFeedback.Web.Services.OuterApi;
using SFA.DAS.Http.Configuration;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public static class ServicesStartup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddTransient<ApprenticeFeedbackApiClient>();

            return services;
        }

        public static IServiceCollection AddOuterApi(this IServiceCollection services, OuterApiConfiguration configuration)
        {
            services.AddTransient<Http.MessageHandlers.DefaultHeadersHandler>();
            services.AddTransient<Http.MessageHandlers.LoggingMessageHandler>();
            services.AddTransient<Http.MessageHandlers.ApimHeadersHandler>();

            services
                .AddRestEaseClient<IOuterApiClient>(configuration.ApiBaseUrl)
                .AddHttpMessageHandler<Http.MessageHandlers.DefaultHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.ApimHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.LoggingMessageHandler>(); 

            services.AddTransient<IApimClientConfiguration>((_) => configuration);

            return services;
        }
    }
}
