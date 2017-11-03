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

        public string RepositoryItemPath { get; set; }
    }
}
