using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace bp.AdoMessageRelay
{
    public static class Relay
    {
      [FunctionName("MessageRelay")]
      [return: EventHub("outputEventHubMessage", Connection = "eh_conn")]
      public static string Run([ServiceBusTrigger("queue1", Connection = "sb_conn")] string myQueueItem, ILogger log)
      {
         log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
         return myQueueItem;
      }
    }
}