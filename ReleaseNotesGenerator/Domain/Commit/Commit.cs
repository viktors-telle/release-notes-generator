namespace ReleaseNotesGenerator.Domain.Commit
{
    public class Commit
    {       
        public string CommitId { get; set; }

        public Author Author { get; set; }

        public Author Committer { get; set; }

        public string Comment { get; set; }

        public bool CommentTruncated { get; set; }        
    }
}
