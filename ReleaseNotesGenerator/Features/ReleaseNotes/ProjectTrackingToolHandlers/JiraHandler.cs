using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Features.ProjectTrackingTools;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.ProjectTrackingToolHandlers
{
    public class JiraHandler : IProjectTrackingToolHandler
    {
        public Task<IList<WorkItem.WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool, string[] workItemIds)
        {
            throw new NotImplementedException();
        }

        public string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem.WorkItem> workItems)
        {
            throw new NotImplementedException();
        }
    }
}
