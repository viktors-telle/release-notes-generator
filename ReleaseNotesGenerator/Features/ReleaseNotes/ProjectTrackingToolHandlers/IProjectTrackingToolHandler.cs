using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;
using ReleaseNotesGenerator.Domain.WorkItem;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.ProjectTrackingToolHandlers
{
    public interface IProjectTrackingToolHandler
    {
        Task<IList<WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool, string[] workItemIds);

        string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem> workItems);
    }
}