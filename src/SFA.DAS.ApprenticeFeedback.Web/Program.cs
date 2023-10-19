using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using SFA.DAS.ApprenticeFeedback.Web.Startup;
using SFA.DAS.NServiceBus.Configuration.MicrosoftDependencyInjection;

namespace SFA.DAS.ApprenticeFeedback.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogStartup.ConfigureNLog();
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<ApplicationStartup>()
                .UseNServiceBusContainer()
                .UseNLog();
    }
}
