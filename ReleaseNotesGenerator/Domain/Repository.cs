using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReleaseNotesGenerator.Enums;

namespace ReleaseNotesGenerator.Domain
{
    public class Repository : EntityBase<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }        

        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        public RepositoryType RepositoryType { get; set; }

        [Required]
        public int ProjectTrackingToolId { get; set; }

        public ProjectTrackingTool ProjectTrackingTool { get; set; }

        public List<Branch> Branches { get; set; }        
    }
}