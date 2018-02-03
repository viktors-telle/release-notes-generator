using System.ComponentModel.DataAnnotations;

namespace ReleaseNotesGenerator.Features.Email
{
    public class EmailRequest
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string[] To { get; set; }

        public string[] Cc { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string RepositoryName { get; set; }

        [Required]
        public string BranchName { get; set; }
    }
}