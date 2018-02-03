using System;
using ReleaseNotesGenerator.Features.SourceCodeRepositories;

namespace ReleaseNotesGenerator.Features.ReleaseNotes
{
    public class ReleaseNote : EntityBase<int>
    {
        public string Notes { get; set; }

        public DateTime Created { get; set; }

        public int RepositoryId { get; set; }

        public Repository Repository { get; set; }

        public string RepositoryPath { get; set; }

        public string BranchName { get; set; }

        public DateTime From { get; set; }

        public DateTime Until { get; set; }
    }
}