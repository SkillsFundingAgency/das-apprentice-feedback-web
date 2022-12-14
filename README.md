## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# Apprentice Feedback Web
<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status/das-apprentice-feedback-web?repoName=SkillsFundingAgency%2Fdas-apprentice-feedback-web&branchName=master)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=2539&repoName=SkillsFundingAgency%2Fdas-apprentice-feedback-web&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-apprentice-feedback-web&metric=alert_status)](https://sonarcloud.io/project/overview?id=SkillsFundingAgency_das-apprentice-feedback-web)
[![Jira Project](https://img.shields.io/badge/Jira-Project-blue)](https://skillsfundingagency.atlassian.net/browse/QF-72)
[![Confluence Project](https://img.shields.io/badge/Confluence-Project-blue)](https://skillsfundingagency.atlassian.net/wiki/spaces/NDL/pages/3776446580/Apprentice+Feedback+-+QF)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

This repository represents the Apprentice Feedback Web code base. Apprentice Feedback is a service that allows apprentices to provide feedback on their training providers. The apprentice is able to submit feedback via the ad hoc journey, or an emailing journey. Either way, the UI code base is the `das-apprentice-feedback-web` repository, the innner api is the `das-apprentice-feedback-api` repository, and the outer API code base is in the `das-apim-endpoints` repository within the `ApprenticeFeedback` project. It should be noted that this service is integrated into the apprentice account which means to run it locally you need other services running simultaneously. 

## Developer Setup
### Requirements

In order to run this solution locally you will need the following:
* [.net 6.0](https://www.microsoft.com/net/download/)
* (VS Code Only) [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)
* [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
* [Azurite](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite) (previously known as Azure Storage Emulator)

### Environment Setup

* **appsettings.development.json** - add the following to your local appsettings.development.json file.
```json
{
  "Authentication": {
    "MetadataAddress": "https://login.at-aas.apprenticeships.education.gov.uk/" 
  },
  "ApplicationUrls": {
    "ApprenticeHomeUrl": "https://localhost:5001",
    "ApprenticeAccountsUrl": "https://localhost:7080",
    "ApprenticeCommitmentsUrl": "https://localhost:7070",
    "ApprenticeLoginUrl": "https://localhost:9999",
    "ApprenticeFeedbackUrl":"https://localhost:7090"
  },
  "ApprenticeFeedbackOuterApi": {
    "ApiBaseUrl": "https://localhost:44307/",
    "SubscriptionKey": "secret",
    "ApiVersion": "1.0"
  },
  "Zendesk" : {
    "ZendeskSectionId": "",
    "ZendeskSnippetKey": "",
    "ZendeskCobrowsingSnippetKey": ""
  },
  "AppSettings":{
    "FindApprenticeshipTrainingBaseUrl": "https://findapprenticeshiptraining.apprenticeships.education.gov.uk/"
  },
  "ConnectionStrings": {
    "RedisConnectionString": "localhost",
    "DataProtectionKeysDatabase": "DefaultDatabase=3"
  },
  "GoogleAnalytics": {
    "GoogleTagManagerId": "GTM-MP5RSWL"
  },
}
```

* **Azure Table Storage Explorer** - There is a choice on whether to add the row key and data to the Azure Table Storage Explorer or have that config in the appsettings.development.json file. Either will work. 

    Azure Table Storage config

    Row Key: SFA.DAS.ApprenticeFeedback.Web_1.0

    Partition Key: LOCAL

### Running

* Start Azurite e.g. using a command `C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator>AzureStorageEmulator.exe start`
* Solutions to run in conjunction - To get this solution running successfully you will also need the following solutions running:
    * `das-apprentice-feedback-api` and `das-apim-endpoints\ApprenticeFeedback` for the inner API and outer API respectively in the Apprentice Feedback service.
    * `das-apprentice-login-service` to get through the authentication for the frontend. 
    * `das-apprentice-accounts-api` for the login connected to the authentication on the frontend. 
* Run the solution 

### Tests

This codebase includes unit tests and acceptance tests. These are all in seperate projects aptly named and kept in the 'Tests' folder.

#### Unit Tests

There are several unit test projects in the solution built using C#, .net 6.0, FluentAssertions, Moq, NUnit, and AutoFixture.
* `SFA.DAS.ApprenticeFeedback.Application.UnitTests`
* `SFA.DAS.ApprenticeFeedback.Domain.UnitTests`
* `SFA.DAS.ApprenticeFeedback.Infrastructure.UnitTests`
* `SFA.DAS.ApprenticeFeedback.Web.UnitTests`

#### Acceptance Tests

There is one acceptance test project, `SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests`, and it tests the web project. 