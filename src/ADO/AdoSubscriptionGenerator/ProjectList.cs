using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace bp.AdoSubscriptionGenerator
{
   /// <summary>
   /// Gets a list of ADO projects with minimal detail
   /// </summary>
   public class ProjectList : IAdoList
   {
      private readonly VssConnection _connection;

      public ProjectList(VssConnection connection)
      {
         _connection = connection;
      }
      /// <summary>
      /// Returns a list of projects via an id and name property
      /// </summary>
      public async Task<IEnumerable<(string, string)>> List()
      {
         // Get the project and team clients
         ProjectHttpClient projectClient = _connection.GetClient<ProjectHttpClient>();

         // Call to get the list of projects
         IEnumerable<TeamProjectReference> projects = await projectClient.GetProjects();

         return projects.Select(a => (a.Id.ToString(), a.Name));
      }
   }
}
