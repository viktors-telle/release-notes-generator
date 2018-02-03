namespace ReleaseNotesGenerator.Features.ReleaseNotes.Commit
{
    public class Commit
    {
        private string _comment;

        public string CommitId { get; set; }

        public Author Author { get; set; }

        public Author Committer { get; set; }

        public string Comment
        {
            get
            {
                return !string.IsNullOrEmpty(_comment) ? _comment : Message;
            }
            set { _comment = value; }
        }

        public bool CommentTruncated { get; set; }

        public string Message { get; set; }

        public string Url { get; set; }
    }
}
