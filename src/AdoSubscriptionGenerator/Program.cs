using CommandLine;
using System;
using System.Threading.Tasks;

namespace bp.AdoSubscriptionGenerator
{
   class Program
   {
      static async Task Main(string[] args)
      {
         // build in the project override and the oppo to do delete as well as subscribe

         var options = await Parser.Default.ParseArguments<ConfigurationWorkflow>(args)
            .WithParsedAsync<ConfigurationWorkflow>(async (opt) =>
            {
               var workflow = new ConfigurationWorkflow();
               var projectList = await workflow.GetProjectList();

               var subscriptionList = await workflow.GetSubscriptionList();
               var publisherList = await workflow.GetPublisherList();
               foreach (var project in projectList)
               {
                  if(!String.IsNullOrEmpty(opt.ProjectOverride) && project.Item1 == opt.ProjectOverride)
                    {
                        if (opt.SubscriptionControl == "create")
                            await foreach (var subscription in workflow.AddSubscriptions(project.Item1))
                            {
                                Console.WriteLine("Create subscription called for workflow");
                            }
                        else if (opt.SubscriptionControl == "Delete" && !String.IsNullOrEmpty(opt.QueueName))
                            await foreach (var subscription in workflow.DeleteSubscriptions(project.Item1))
                            {
                                Console.WriteLine("Delete subscription called for workflow");
                            }
                  }
                    else
                    {
                        Console.WriteLine("Skipping project {0} with id {1}", project.Item2, project.Item1);
                    }
                  
               }
               Console.WriteLine("Finished, press any key to continue ...");
               Console.Read();
            });
      }  
   }
}
