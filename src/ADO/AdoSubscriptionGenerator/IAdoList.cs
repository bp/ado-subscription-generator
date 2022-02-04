using System.Collections.Generic;
using System.Threading.Tasks;

namespace bp.AdoSubscriptionGenerator
{
   /// <summary>
   /// Interface to build lists from ADO objects
   /// </summary>
   public interface IAdoList
   {
      /// <summary>
      /// Gives back a list of items e.g. projects, teams etc.
      /// </summary>
      /// <returns>A tuple containing a guid id and a name</returns>
      public Task<IEnumerable<(string, string)>> List();
   }
}
