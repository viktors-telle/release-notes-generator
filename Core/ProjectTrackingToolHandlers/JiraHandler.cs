using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;
using ReleaseNotesGenerator.Domain.WorkItem;

namespace ReleaseNotesGenerator.Core.ProjectTrackingToolHandlers
{
    public class JiraHandler : IProjectTrackingToolHandler
    {
        public Task<IList<WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<string> workItemIds)
        {
            throw new System.NotImplementedException();
        }

        public string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem> workItems)
        {
            throw new System.NotImplementedException();
        }
    }
}