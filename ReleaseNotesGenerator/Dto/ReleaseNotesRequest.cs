using System;
using System.ComponentModel.DataAnnotations;

namespace ReleaseNotesGenerator.Dto
{
    public class ReleaseNotesRequest
    {
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string RepositoryName { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime Until { get; set; }

        public string RepositoryPath { get; set; }
    }
}
