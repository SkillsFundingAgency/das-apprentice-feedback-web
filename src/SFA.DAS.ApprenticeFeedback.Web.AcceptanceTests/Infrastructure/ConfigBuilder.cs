//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Memory;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Infrastructure
//{
//    public static class ConfigBuilder
//    {
//        public static IConfigurationRoot GenerateConfiguration()
//        {
//            var configSource = new MemoryConfigurationSource
//            {
//                InitialData = new[]
//                {
//                    new KeyValuePair<string, string>("ConfigurationStorageConnectionString", "UseDevelopmentStorage=true;"),
//                    new KeyValuePair<string, string>("ConfigNames", "SFA.DAS.ApprenticeFeedback.Web"),
//                    new KeyValuePair<string, string>("EnvironmentName", "DEV"),
//                    new KeyValuePair<string, string>("Version", "1.0"),

//                    new KeyValuePair<string, string>("Authentication:MetadataAddress", ""),
//                    new KeyValuePair<string, string>("ApprenticeFeedbackOuterApi:ApiBaseUrl", "https://localhost:6021/"),
//                    new KeyValuePair<string, string>("ApplicationUrls:ApprenticeHomeUrl", "https://home/"),
//                    new KeyValuePair<string, string>("ApplicationUrls:ApprenticeAccountsUrl", "https://account/"),
//                    new KeyValuePair<string, string>("ApplicationUrls:ApprenticeCommitmentsUrl", "http://commitments/"),
//                    new KeyValuePair<string, string>("ApplicationUrls:ApprenticeLoginUrl", "https://login/")
//                }
//            };

//            var provider = new MemoryConfigurationProvider(configSource);

//            return new ConfigurationRoot(new List<IConfigurationProvider> { provider });
//        }
//    }
//}
