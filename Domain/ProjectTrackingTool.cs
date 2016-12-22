using System.Collections.Generic;
using ReleaesNotesGenerator.Common.Enums;

namespace ReleaseNotesGenerator.Domain
{
    public class ProjectTrackingTool : EntityBase<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }        

        public string AccessToken { get; set; }

        public string ProjectName { get; set; }

        public ProjectTrackingToolType Type { get; set; }

        public List<Repository> Repositories { get; set; }
    }
}
