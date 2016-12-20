using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Core.ProjectTrackingToolHandlers
{
    public class JiraHandler : IProjectTrackingToolHandler
    {
        public Task<WorkItem> GetWorkItems(IEnumerable<string> workItemIds)
        {
            throw new System.NotImplementedException();
        }
    }
}