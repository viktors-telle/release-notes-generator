namespace ReleaseNotesGenerator.Features.ReleaseNotes.Commit
{
    public class GitHubCommitResponse
    {
        public string Sha { get; set; }

        public Commit Commit { get; set; }
    }
}