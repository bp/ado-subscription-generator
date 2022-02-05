using System.Collections.Generic;

namespace bp.AdoSubscriptionGenerator
{
   /// <summary>
   /// Wrapper class to hold details of the info needed for a servicehook
   /// </summary>
   public class ServiceHookEventDescription
   {
      public ServiceHookEventDescription(string friendlyName, string eventId, 
         string publisherId, string resourceName)
      {
         FriendlyName = friendlyName;
         EventId = eventId;
         PublisherId = publisherId;
         ResourceName = resourceName;
      }
      /// <summary>
      /// The friendly name of the hook
      /// </summary>
      public string FriendlyName { get; private set; }
      /// <summary>
      /// The event id used for the trigger
      /// </summary>
      public string EventId { get; private set; }
      /// <summary>
      /// The publisher id, e.g. rm 
      /// </summary>
      public string PublisherId { get; private set; }
      /// <summary>
      /// The name of the required resource
      /// </summary>
      public string ResourceName { get; private set; }
      /// <summary>
      /// Returns a list of triggers for builds, code, pipelines and workitems 
      /// </summary>
      public static List<ServiceHookEventDescription> ServiceHookEventDescriptionList()
      {
         return new List<ServiceHookEventDescription>()
         {
           new ServiceHookEventDescription("Build completed", "build.complete", "tfs", "build"),
           new ServiceHookEventDescription("Release abandoned", "ms.vss-release.release-abandoned-event", "rm", "resource"),
           new ServiceHookEventDescription("Release created", "ms.vss-release.release-created-event", "rm", "resource"),
           new ServiceHookEventDescription("Release deployment approval completed", "ms.vss-release.deployment-approval-completed-event", "rm", "resource"),
           new ServiceHookEventDescription("Release deployment approval pending", "ms.vss-release.deployment-approval-pending-event", "rm", "resource"),
           new ServiceHookEventDescription("Release deployment completed", "ms.vss-release.deployment-completed-event", "rm", "resource"),
           new ServiceHookEventDescription("Release deployment started", "ms.vss-release.deployment-started-event", "rm", "resource"),
           new ServiceHookEventDescription("Run state changed", "ms.vss-pipelines.run-state-changed-event", "pipelines", "resource"),
           new ServiceHookEventDescription("Run stage state changed", "ms.vss-pipelines.stage-state-changed-event", "pipelines", "resource"),
           new ServiceHookEventDescription("Run stage waiting for approval", "ms.vss-pipelinechecks-events.approval-pending", "pipelines", "resource"),
           new ServiceHookEventDescription("Run stage approval completed", "ms.vss-pipelinechecks-events.approval-completed", "pipelines", "resource"),
           new ServiceHookEventDescription("Code checked in", "tfvc.checkin", "tfs", "changeset"),
           new ServiceHookEventDescription("Code pushed", "git.push", "tfs", "push"),
           new ServiceHookEventDescription("Pull request created", "git.pullrequest.created", "tfs", "pullrequest"),
           new ServiceHookEventDescription("Pull request merge commit created", "git.pullrequest.merged", "tfs", "pullrequest"),
           new ServiceHookEventDescription("Work item created", "workitem.created", "tfs", "workitem"),
           new ServiceHookEventDescription("Work item deleted", "workitem.deleted", "tfs", "resource"),
           new ServiceHookEventDescription("Work item restored", "workitem.restored", "tfs", "resource"),
           new ServiceHookEventDescription("Work item updated", "workitem.updated", "tfs", "workitem"),
           new ServiceHookEventDescription("Work item commented on", "workitem.commented", "tfs", "workitem")
         };
      }
   }

   
}