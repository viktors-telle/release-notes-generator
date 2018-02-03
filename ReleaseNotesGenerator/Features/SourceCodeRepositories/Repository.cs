using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReleaseNotesGenerator.Enums;
using ReleaseNotesGenerator.Features.Projects;
using ReleaseNotesGenerator.Features.ProjectTrackingTools;
using ReleaseNotesGenerator.Features.ReleaseNotes;

namespace ReleaseNotesGenerator.Features.SourceCodeRepositories
{
    public class Repository : EntityBase<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public string Owner { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        public RepositoryType RepositoryType { get; set; }

        public int? ProjectTrackingToolId { get; set; }

        public ProjectTrackingTool ProjectTrackingTool { get; set; }   
        
        public List<ReleaseNote> ReleaseNotes { get; set; }
    }
}