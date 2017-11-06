using System;

namespace ReleaseNotesGenerator.Domain
{
    public class ReleaseNote : EntityBase<int>
    {
        public string Notes { get; set; }

        public DateTime Created { get; set; }

        public int RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}