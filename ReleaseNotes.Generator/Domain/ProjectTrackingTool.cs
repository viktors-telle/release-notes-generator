using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReleaseNotes.Generator.Enums;

namespace ReleaseNotes.Generator.Domain
{
    public class ProjectTrackingTool : EntityBase<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }        

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public ProjectTrackingToolType Type { get; set; }

        public List<Repository> Repositories { get; set; }
    }
}