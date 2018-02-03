namespace ReleaseNotesGenerator.Features.ReleaseNotes.Commit
{
    public class GitHubCommitResponse
    {
        public string Sha { get; set; }

        public Features.ReleaseNotes.Commit.Commit Commit { get; set; }
    }
}