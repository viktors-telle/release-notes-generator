using System.Collections.Generic;

namespace ReleaseNotesGenerator.Domain
{
    public class Repository : EntityBase<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public int RepositoryTypeId { get; set; }

        public virtual RepositoryType RepositoryType { get; set; }

        public int ProjectTrackingToolId { get; set; }

        public virtual ProjectTrackingTool ProjectTrackingTool { get; set; }

        public List<Branch> Branches { get; set; }
    }
}
