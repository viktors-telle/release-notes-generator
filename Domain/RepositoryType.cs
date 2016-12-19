using System.Collections.Generic;

namespace ReleaseNotesGenerator.Domain
{
    public class RepositoryType : EntityBase<int>
    {
        public string Name { get; set; }

        public ReleaesNotesGenerator.Common.Enums.RepositoryType Type { get; set; }

        public List<Repository> Repositories { get; set; }
    }
}
