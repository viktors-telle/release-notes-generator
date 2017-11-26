namespace ReleaseNotesGenerator.Domain.Commit
{
    public class GitHubCommitResponse
    {
        public string Sha { get; set; }

        public Commit Commit { get; set; }
    }
}