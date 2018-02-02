using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;
using ReleaseNotesGenerator.Domain.WorkItem;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.ProjectTrackingToolHandlers
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
