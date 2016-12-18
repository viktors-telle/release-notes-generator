namespace ReleaseNotesGenerator.Common.Models
{
    public class ReleaseNotesRequest
    {
        public int ProjectId { get; set; }

        public int RepositoryId { get; set; }

        public string BranchName { get; set; }
    }
}
