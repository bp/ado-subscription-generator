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
   public class SubscriptionDelete
   {
      private readonly VssConnection _connection;

      public SubscriptionDelete(VssConnection connection)
      {
         _connection = connection;
      }
      /// <summary>
      /// Removes a subscription given a set of details using the event hub triggers
      /// </summary>
      /// <returns>A guid subscription id</returns>
      public async Task<string> Delete(SubscriptionDetails details)
      {
         throw new NotImplementedException();
      }
   }
}

