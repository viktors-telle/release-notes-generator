using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Features.ProjectTrackingTools;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.ProjectTrackingToolHandlers
{
    public interface IProjectTrackingToolHandler
    {
        Task<IList<WorkItem.WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool, string[] workItemIds);

        string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem.WorkItem> workItems);
    }
}