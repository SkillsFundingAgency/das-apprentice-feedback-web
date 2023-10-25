using Newtonsoft.Json;
using NServiceBus;
using SFA.DAS.ApprenticeFeedback.Messages.Events;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;
using System.Reflection;

namespace SFA.DAS.ApprenticeFeedback.Web.NServiceBusListener
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "NServiceBusListener";

            // it is likely that this is running without the authority to create subscriptions on an azure instance, so 
            // the endpoint name used here is the name used by the SFA.DAS.ApprenticeFeedback.Jobs as this subscription has
            // already been created to the topic and is the subscription which will be receiving the ApprenticeEmailClickEvent
            // event when deployed so it will be used for convenience to avoid having to create a subscription in this code
            var endpointConfiguration = new EndpointConfiguration("SFA.DAS.ApprenticeFeedback")
                .UseMessageConventions()
                .UseNewtonsoftJsonSerializer();

            // read command line arguments to get the connection string e.g. Endpoint=sb://some-identifier.servicebus.windows.net/
            var args = Environment.GetCommandLineArgs();
            var connectionStringArg = args.FirstOrDefault(a => a.StartsWith("NServiceBusConnectionString="));
            var connectionStringValue = connectionStringArg?.Substring("NServiceBusConnectionString=".Length);

            // read the NServiceBus License file - obtained from https://particular.net/license/nservicebus?v=7.2.3&t=0&p=windows
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            var licenseFilePath = Path.Combine(currentDirectory, "License.xml");

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

            if (File.Exists(licenseFilePath))
            {
                string licenseContent = File.ReadAllText(licenseFilePath);
                endpointConfiguration.License(licenseContent);
            }
            
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

    }

    /// <summary>
    /// The ApprenticeEmailClickEvent is displayed as '46cd067d-1f53-715e-2b4c-adaac1cc7ff1' in
    /// the Azure portal as that is the unique name when shortened by the SFA.DAS.NServiceBus package
    /// to fit within the 50 character limit of an Azure service bus names
    /// </summary>
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