using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Domain;
using ReleaseNotes.Generator.Domain.WorkItem;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class JiraHandler : IProjectTrackingToolHandler
    {
        public Task<IList<WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool, string[] workItemIds)
        {
            throw new NotImplementedException();
        }

        public string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem> workItems)
        {
            throw new NotImplementedException();
        }
    }
}
