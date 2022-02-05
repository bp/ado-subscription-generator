using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.Services.Notifications.WebApi.Clients;
using Microsoft.VisualStudio.Services.Notifications.WebApi;

namespace bp.AdoSubscriptionGenerator
{
   /// <summary>
   /// Gets a list of subscription events 
   /// </summary>
   public class SubscriptionEventTypeList : IAdoList
   {
      private readonly VssConnection _connection;

      public SubscriptionEventTypeList(VssConnection connection)
      {
         _connection = connection;
      }
      /// <summary>
      /// Lists the ids and names in subscription events
      /// </summary>
      public async Task<IEnumerable<(string, string)>> List()
      {
         NotificationHttpClient notificationClient = _connection.GetClient<NotificationHttpClient>();
         IEnumerable<NotificationEventType> notificationEvents = await notificationClient.ListEventTypesAsync();

         return notificationEvents.Select(a => (a.Id, a.Name));
      }
   }
}
