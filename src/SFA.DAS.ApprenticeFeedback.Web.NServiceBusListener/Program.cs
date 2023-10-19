using Newtonsoft.Json;
using NServiceBus;
using SFA.DAS.ApprenticeFeedback.Messages.Events;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;

namespace SFA.DAS.ApprenticeFeedback.Web.NServiceBusListener
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "NServiceBusListener";

            var endpointConfiguration = new EndpointConfiguration("SFA.DAS.ApprenticeFeedback.Web.NServiceBusListener")
                .UseMessageConventions()
                .UseNewtonsoftJsonSerializer();

            // Read command line arguments
            var args = Environment.GetCommandLineArgs();
            var connectionStringArg = args.FirstOrDefault(a => a.StartsWith("NServiceBusConnectionString="));
            var connectionStringValue = connectionStringArg?.Split('=')[1];

            if (string.IsNullOrEmpty(connectionStringValue))
            {
                throw new InvalidOperationException("NServiceBusConnectionString argument not provided.");
            }
            else if (connectionStringValue.Equals("UseLearningEndpoint", StringComparison.CurrentCultureIgnoreCase))
            {
                endpointConfiguration.UseTransport<LearningTransport>();
            }
            else
            {
                endpointConfiguration.UseAzureServiceBusTransport(connectionStringValue);
            }

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

    }

    public class ApprenticeEmailClickEventHandler : IHandleMessages<ApprenticeEmailClickEvent>
    {
        public Task Handle(ApprenticeEmailClickEvent message, IMessageHandlerContext context)
        {
            var json = JsonConvert.SerializeObject(message);
            Console.WriteLine($"Received ApprenticeEmailClickEvent Message:{json}");

            return Task.CompletedTask;
        }
    }
}