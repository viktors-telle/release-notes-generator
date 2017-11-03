using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotes.Generator.Domain;
using ReleaseNotes.Generator.Domain.WorkItem;

namespace ReleaseNotes.Generator.Components.Interfaces
{
    public interface IProjectTrackingToolHandler
    {
        Task<IList<WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool, string[] workItemIds);

        string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem> workItems);
    }
}