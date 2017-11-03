using System.Collections.Generic;

namespace ReleaseNotesGenerator.Domain.WorkItem
{
    public class WorkItemResponse
    {
        public int Count { get; set; }
        public List<WorkItem> Value { get; set; }
    }
}
