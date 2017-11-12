using System;

namespace ReleaseNotesGenerator.Domain.Commit
{
    public class CommitQuery
    {
        public string Url { get; set; }

        public string Owner { get; set; }

        public string BranchName { get; set; }

        public string RepositoryName { get; set; }

        public string RepositoryPath { get; set; }

        public DateTime From { get; set; }

        public DateTime Until { get; set; }
    }
}