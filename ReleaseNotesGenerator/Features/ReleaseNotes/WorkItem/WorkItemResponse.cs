using System.Collections.Generic;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.WorkItem
{
    public class WorkItemResponse
    {
        public int Count { get; set; }
        public List<WorkItem> Value { get; set; }
    }
}
