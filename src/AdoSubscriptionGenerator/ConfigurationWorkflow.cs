using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace bp.AdoSubscriptionGenerator
{
   internal class ConfigurationWorkflow
   {
      private readonly VssConnection _connection;
      /// <summary>
      /// Constructs a connection to Azure Devops
      /// </summary>
      public ConfigurationWorkflow()
      {
         _connection = GetConnection();
      }
      private IConfiguration GetConfigArguments()
      {
         return new ConfigurationBuilder()
            .AddJsonFile("config.json", true, true)
            .Build();
      }
      /// <summary>
      /// Takes arguments to either create or delete a subscription
      /// </summary>
      [Option('s', "subscription-control", Required = true, HelpText = "Creates or deletes subscriptions")]
      public string SubscriptionControl { get; set; }
      /// <summary>
      /// Used for testing to send a single project to the 
      /// </summary>
      [Option('p', "project-override", Required = false, HelpText = "Builds a test override for a specific project")]
      public string ProjectOverride { get; set; }
      /// <summary>
      /// The service bus connection string 
      /// </summary>
      public string ConnectionString { get { return GetConfigArguments()["connectionString"]; } }
      /// <summary>
      /// The queue name to register the trigger with
      /// </summary>
      public string QueueName { get { return GetConfigArguments()["queueName"]; } }
      private VssBasicCredential GetCredentials()
      {
         return new VssBasicCredential(string.Empty, GetConfigArguments()["pat"]);
      }

      private VssConnection GetConnection()
      {
         return new VssConnection(new Uri(GetConfigArguments()["endpoint"]), GetCredentials());
      }
      
      internal async Task<List<(string, string)>> GetProjectList()
      {
         var projectList = new ProjectList(_connection);
         AddConsoleHeader("Starting project list get");
         var list = (await projectList.List()).ToList();
         list.ForEach(a => Console.WriteLine("{0}:{1}", a.Item1, a.Item2));
         AddConsoleHeader("Ending project list get");
         return list;
      }

      internal async Task<List<(string, string)>> GetSubscriptionList()
      {
         var subscriptionList = new SubscriptionEventTypeList(_connection);
         AddConsoleHeader("Starting subscription list get");
         var list = (await subscriptionList.List()).ToList();
         list.ForEach(a => Console.WriteLine(a.Item1));
         AddConsoleHeader("Ending subscription list get");
         return list;
      }

      internal async Task<List<(string, string)>> GetPublisherList()
      {
         var publisherList = new ServiceHooksPublisherEventList(_connection);
         AddConsoleHeader("Starting publisher list");
         var list = (await publisherList.List()).ToList();
         list.ForEach(a => Console.WriteLine("{0}:{1}", a.Item1, a.Item2));
         AddConsoleHeader("Ending publisher list");
         return list;
      }

      internal async IAsyncEnumerable<string> AddSubscriptions(string projectId)
      {
         var subscriberAdd = new SubscriptionAdd(_connection);
         string id = null;
         foreach (var eventType in ServiceHookEventDescription.ServiceHookEventDescriptionList())
         {
            try
            {
               var details = new SubscriptionDetails()
               {
                  PublisherId = eventType.PublisherId,
                  ProjectId = projectId,
                  EventId = eventType.EventId,
                  ConnectionString = this.ConnectionString,
                  QueueName = this.QueueName
               };
               id = await subscriberAdd.Add(details);
               Console.WriteLine($"Register event: {eventType.EventId} for project {projectId} with subscription id: {id}");
            }
            catch
            {
               Console.WriteLine($"Publisher {eventType.PublisherId} failed for project {projectId} and event {eventType.EventId}");
            }
            yield return id;
         }
      }
      
      internal async IAsyncEnumerable<string> DeleteSubscriptions(string projectId)
      {
         var subscriberAdd = new SubscriptionDelete(_connection);
         string id = null;
         foreach (var eventType in ServiceHookEventDescription.ServiceHookEventDescriptionList())
         {
            try
            {
               var details = new SubscriptionDetails()
               {
                  PublisherId = eventType.PublisherId,
                  ProjectId = projectId,
                  EventId = eventType.EventId,
                  ConnectionString = this.ConnectionString,
                  QueueName = this.QueueName
               };
               id = await subscriberAdd.Delete(details);
               Console.WriteLine($"Register event: {eventType.EventId} for project {projectId} with subscription id: {id}");
            }
            catch
            {
               Console.WriteLine($"Publisher {eventType.PublisherId} failed for project {projectId} and event {eventType.EventId}");
            }
            yield return id;
         }
      }

      private static void AddConsoleHeader(string headerValue)
      {
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine(headerValue);
         Console.ForegroundColor = ConsoleColor.Green;
      }
   }
}
