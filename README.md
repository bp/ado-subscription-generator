# Azure DevOps Subscription Generator
Subscription Generator is a command line tool for Azure DevOps which helps add subscriptions for the triggers that are enabled for Azure DevOps Service Hooks. The default subscription use is via an Azure Service Bus Queue.

In order to configure ADOSubscriptionGenerator you should compile and ensure that a file called config.json is in the same directory as the .exe file. Config.json contains the following details:

- A Personal Access Token (PAT) token which is organisational-wide and is created per user
- An endpoint to an Azure DevOps instance which is preceded by https://dev.azure.com/
- A connection string to a service bus
- The name of a service bus queue 

The json representation should looked like the following:

```
{
   "pat": "",
   "endpoint": "https://dev.azure.com/bp",
   "connectionString": "",
   "queueName": ""
}
```