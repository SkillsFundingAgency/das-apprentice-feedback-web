using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using SFA.DAS.ApprenticeFeedback.Web.Configuration;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.Authentication.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public static class AuthenticationStartup
    {
        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            AuthenticationConfiguration config,
            IWebHostEnvironment environment)
        {
            services
                .AddApplicationAuthentication(config, environment)
                .AddApplicationAuthorisation();

            services.AddTransient((_) => config);

            return services;
        }

        private static IServiceCollection AddApplicationAuthentication(
           this IServiceCollection services,
           AuthenticationConfiguration config,
           IWebHostEnvironment environment)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            IdentityModelEventSource.ShowPII = true;

            services.AddApprenticeAuthentication(config.MetadataAddress, environment);

            services.AddTransient<IApprenticeAccountProvider, ApprenticeAccountProvider>();

            return services;
        }

        private static IServiceCollection AddApplicationAuthorisation(
            this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.AddRazorPages(o => o.Conventions
                .AuthorizeFolder("/"));
            services.AddRazorPages(options =>
            {
                options.Conventions.ConfigureFilter(factory =>
                {
                    var typeFilterAttribute = new TypeFilterAttribute(typeof(RequiresIdentityConfirmedFilter));
                    return typeFilterAttribute;
                });

                options.Conventions.ConfigureFilter(factory =>
                {
                    var typeFilterAttribute = new TypeFilterAttribute(typeof(IsPrivateBetaFilter));
                    return typeFilterAttribute;
                });
            });
            services.AddScoped<AuthenticatedUser>();
            services.AddScoped(s => s
                .GetRequiredService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }
}
