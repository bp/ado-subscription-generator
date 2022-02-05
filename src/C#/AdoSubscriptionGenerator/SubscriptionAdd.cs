using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bp.AdoSubscriptionGenerator
{
   /// <summary>
   /// Adds a subscription via a service hook 
   /// </summary>
   public class SubscriptionAdd
   {
      private readonly VssConnection _connection;

      public SubscriptionAdd(VssConnection connection)
      {
         _connection = connection;
      }
      /// <summary>
      /// Adds a subscription given a set of details using the event hub triggers
      /// </summary>
      /// <returns>A guid subscription id</returns>
      public async Task<string> Add(SubscriptionDetails details)
      {
         var serviceHooksClient = _connection.GetClient<ServiceHooksPublisherHttpClient>();

         Subscription subscriptionParameters = new Subscription()
         {
            ConsumerId = "azureServiceBus",
            ConsumerActionId = "serviceBusQueueSend",
            ConsumerInputs = new Dictionary<string, string>
                    {
                        { "connectionString", details.ConnectionString },
                        { "queueName", details.QueueName }
                    },
            PublisherInputs = new Dictionary<string, string>
                {
                    { "projectId", details.ProjectId }
                },
            EventType = details.EventId,
            PublisherId = details.PublisherId
         };
            String id = "";

            try
            {
                Subscription newSubscription = await serviceHooksClient.CreateSubscriptionAsync(subscriptionParameters);
                id = newSubscription.Id.ToString();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return id;
      }
   }

   public class SubscriptionDetails
   {
      public string PublisherId { get; set; }
      public string ProjectId { get; set; }
      public string EventId { get; set; }
      public string ConnectionString { get; set; }
      public string QueueName { get; set;  }
   }
}

