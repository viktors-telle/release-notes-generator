using System.Collections.Generic;
using ReleaesNotesGenerator.Common.Enums;

namespace ReleaseNotesGenerator.Domain
{
    public class ProjectTrackingTool : EntityBase<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ProjectTrackingToolType Type { get; set; }

        public List<Repository> Repositories { get; set; }
    }
}
