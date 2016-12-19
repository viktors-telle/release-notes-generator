using System.Collections.Generic;

namespace ReleaseNotesGenerator.Domain
{
    public class Repository : EntityBase<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string AccessToken { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int RepositoryTypeId { get; set; }

        public RepositoryType RepositoryType { get; set; }

        public int ProjectTrackingToolId { get; set; }

        public ProjectTrackingTool ProjectTrackingTool { get; set; }

        public List<Branch> Branches { get; set; }
    }
}
