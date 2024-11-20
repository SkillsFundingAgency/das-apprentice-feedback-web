using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SFA.DAS.ApprenticeFeedback.Web.Startup;
using SFA.DAS.NServiceBus.Configuration.MicrosoftDependencyInjection;

namespace SFA.DAS.ApprenticeFeedback.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<ApplicationStartup>()
                .UseNServiceBusContainer();
    }
}
