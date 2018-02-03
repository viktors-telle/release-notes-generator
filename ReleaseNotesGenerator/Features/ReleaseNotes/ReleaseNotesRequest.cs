using System;
using System.ComponentModel.DataAnnotations;

namespace ReleaseNotesGenerator.Features.ReleaseNotes
{
    public class ReleaseNotesRequest
    {
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string RepositoryName { get; set; }

        [Required]
        public string BranchName { get; set; }

        public DateTime From { get; set; }

        public DateTime Until { get; set; }

        public string RepositoryPath { get; set; }
    }
}
