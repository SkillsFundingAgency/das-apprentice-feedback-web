using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticeFeedback.Web.Filters;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using SFA.DAS.ApprenticePortal.Authentication.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public static class AuthenticationStartup
    {
        public static void AddGovLoginAuthentication(
            this IServiceCollection services,
            NavigationSectionUrls config,
            IConfiguration configuration)
        {
            if (configuration["EnvironmentName"] != "ACCEPTANCE_TESTS")
            {
                services.AddGovLoginAuthentication(configuration);
            }

            services.AddApplicationAuthorisation();
            services.AddTransient<IApprenticeAccountProvider, ApprenticeAccountProvider>();
            services.AddTransient((_) => config);
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
