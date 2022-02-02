using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticePortal.SharedUi.Startup;
using SFA.DAS.ApprenticeFeedback.Web.Configuration;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticeFeedback.Web.Startup
{
    public class ApplicationStartup
    {
        public ApplicationStartup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appConfig = Configuration.Get<ApplicationConfiguration>();

            services
                .AddAuthentication(appConfig.Authentication, Environment)
                .AddOuterApi(appConfig.ApprenticeFeedbackOuterApi)
                .RegisterServices(Environment);

            services.AddSharedUi(appConfig, options =>
            {
                // ToDo: add apprentice feedback section to shared ui package
                options.SetCurrentNavigationSection(NavigationSection.Home);
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
