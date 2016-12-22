using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;
using ReleaseNotesGenerator.Domain.WorkItem;

namespace ReleaseNotesGenerator.Core.ProjectTrackingToolHandlers
{
    public interface IProjectTrackingToolHandler
    {
        Task<IList<WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<string> workItemIds);

        string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem> workItems);
    }
}