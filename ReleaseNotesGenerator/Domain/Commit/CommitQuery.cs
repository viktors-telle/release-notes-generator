using System;

namespace ReleaseNotesGenerator.Domain.Commit
{
    public class CommitQuery
    {
        public string Url { get; set; }

        public string BranchName { get; set; }

        public string RepositoryName { get; set; }

        public string ItemPath { get; set; }

        public DateTime From { get; set; }

        public DateTime Until { get; set; }
    }
}