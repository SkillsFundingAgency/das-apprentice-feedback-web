using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.Apprentice.Feedback.Infrastructure.SessionService;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Web.Configuration;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Services;
using SFA.DAS.Http.Configuration;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public static class ServicesStartup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddTransient<ApprenticeFeedbackApiClient>();
            services.AddTransient<IApprenticeFeedbackService, ApprenticeFeedbackService>();
            services.AddTransient<IMenuVisibility, MenuVisibility>();

            return services;
        }

        public static IServiceCollection AddOuterApi(this IServiceCollection services, OuterApiConfiguration configuration)
        {
            services.AddTransient<Http.MessageHandlers.DefaultHeadersHandler>();
            services.AddTransient<Http.MessageHandlers.LoggingMessageHandler>();
            services.AddTransient<Http.MessageHandlers.ApimHeadersHandler>();

            services
                .AddRestEaseClient<IApprenticeFeedbackApi>(configuration.ApiBaseUrl)
                .AddHttpMessageHandler<Http.MessageHandlers.DefaultHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.ApimHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.LoggingMessageHandler>(); 

            services.AddTransient<IApimClientConfiguration>((_) => configuration);

            return services;
        }

        public static IServiceCollection AddSessionService(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ISessionService>(x =>
                new SessionService(x.GetService<IHttpContextAccessor>(), environment.EnvironmentName));

            services.AddTransient<IApprenticeFeedbackSessionService>(x =>
                new ApprenticeFeedbackSessionService(x.GetService<ISessionService>()));

            return services;
        }
    }
}
