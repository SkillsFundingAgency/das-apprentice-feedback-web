using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using SFA.DAS.ApprenticeFeedback.Web.Configuration;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

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

            services.AddRazorPages(o => o.Conventions
                .AuthorizeFolder("/")
                .AllowAnonymousToPage("/Index"));
            services.AddScoped<AuthenticatedUser>();
            services.AddScoped(s => s
                .GetRequiredService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }
    public class AuthenticationServiceConfiguration
    {
        public string MetadataAddress { get; set; } = null!;
    }
}
