using Microsoft.AspNetCore.Authorization;
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
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

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
        
        public static void AddGovLoginAuthentication(
            this IServiceCollection services,
            NavigationSectionUrls config,
            IConfiguration configuration)
        {
            services.AddGovLoginAuthentication(configuration);
            services.AddApplicationAuthorisation();
            services.AddTransient<IApprenticeAccountProvider, ApprenticeAccountProvider>();
            services.AddTransient((_) => config);
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

            services.AddRazorPages(options =>
            {
                options.Conventions
                    .AuthorizeFolder("/")
                    .AllowAnonymousToPage("/ping")
                    .AllowAnonymousToPage("/links");

                options.Conventions.ConfigureFilter(context =>
                {
                    if (context.DeclaredModelType.GetCustomAttributes<AllowAnonymousAttribute>(true).Any())
                    {
                        return new DoesNotRequireIdentityConfirmedFilter();
                    }

                    return new TypeFilterAttribute(typeof(RequiresIdentityConfirmedFilter));
                });
            });

            services.AddAntiforgery(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddScoped<AuthenticatedUser>();
            services.AddScoped(s => s
                .GetRequiredService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }
}
