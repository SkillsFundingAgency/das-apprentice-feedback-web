using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Infrastructure;
using SFA.DAS.ApprenticeFeedback.Infrastructure.Session;
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
            services.AddTransient<IApprenticeFeedbackService, ApprenticeFeedbackService>();
            services.AddScoped<IApprenticeFeedbackService, ApprenticeFeedbackService>();
            services.AddScoped<IMenuVisibility, MenuVisibility>();
            services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();
            services.AddTransient<IUrlHelper, UrlHelper>();
            services.AddScoped<IExitSurveySessionService, ExitSurveySessionService>();

            return services;
        }

        public static IServiceCollection AddOuterApi(this IServiceCollection services, OuterApiConfiguration configuration)
        {
            services.AddHealthChecks();
            services.AddScoped<Http.MessageHandlers.DefaultHeadersHandler>();
            services.AddScoped<Http.MessageHandlers.LoggingMessageHandler>();
            services.AddScoped<Http.MessageHandlers.ApimHeadersHandler>();

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

            services.AddTransient<IApprenticeFeedbackSessionService, ApprenticeFeedbackSessionService>();

            return services;
        }
    }
}
