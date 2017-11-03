using System;

namespace ReleaseNotesGenerator.Domain.Commit
{
    public class CommitQuery
    {
        public string Url { get; set; }

        public string BranchName { get; set; }

        public string LastCommitId { get; set; }

        public DateTime? DateTime { get; set; }

        public string RepositoryName { get; set; }

        public string ItemPath { get; set; }
    }
}