using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace bp.AdoSubscriptionGenerator
{
   /// <summary>
   /// Gets a list of publisher and their events
   /// </summary>
   public class ServiceHooksPublisherEventList : IAdoList
   {
      private readonly VssConnection _connection;

      public ServiceHooksPublisherEventList(VssConnection connection)
      {
         _connection = connection;
      }
      /// <summary>
      /// Lists the ifs and names of each of the publishers
      /// </summary>
      public async Task<IEnumerable<(string, string)>> List()
      {
         var publisherClient = _connection.GetClient<ServiceHooksPublisherHttpClient>();
         var serviceHookEvents = await publisherClient.GetPublishersAsync();

         return serviceHookEvents.Select(a => (a.Id, String.Join(',', a.SupportedEvents.Select(b => b.Id).ToList())));
      }
      /// <summary>
      /// Gets an individual publisher based on the name, e.g. rm
      /// </summary>
      public async Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher> Get(string publisherName)
      {
         var publisherClient = _connection.GetClient<ServiceHooksPublisherHttpClient>();
         return await publisherClient.GetPublisherAsync(publisherName);
      }
   }
}
