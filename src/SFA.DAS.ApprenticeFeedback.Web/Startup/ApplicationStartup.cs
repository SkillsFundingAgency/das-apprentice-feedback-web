using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticePortal.SharedUi.Startup;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticeFeedback.Web.Configuration;
using System.IO;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.ApprenticeFeedback.Web.Services;
using SFA.DAS.ApprenticePortal.SharedUi.Zendesk;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public class ApplicationStartup
    {
        public ApplicationStartup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Environment = environment;

            var config = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            config.AddAzureTableStorage(options =>
            {
                options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
                options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                options.EnvironmentName = configuration["EnvironmentName"];
                options.PreFixConfigurationKeys = false;
            });

#if DEBUG
            config.AddJsonFile($"appsettings.Development.json", optional: true);
#endif
            Configuration = config.Build();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appConfig = Configuration.Get<ApplicationConfiguration>();

            services
                .AddApplicationInsightsTelemetry()
                .AddDataProtection(appConfig.ConnectionStrings, Environment)
                .AddAuthentication(appConfig.Authentication, Environment)
                .AddOuterApi(appConfig.ApprenticeFeedbackOuterApi)
                .AddSessionService(Environment)
                .RegisterServices(Environment);

            services.AddTransient((_) => appConfig.AppSettings);

            services.AddSharedUi(appConfig, options =>
            {
                options.SetCurrentNavigationSection(NavigationSection.ApprenticeFeedback);
            });

            services.AddRazorPages();
            services.AddSession();

            services.SetZenDeskConfiguration(appConfig.Zendesk);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
