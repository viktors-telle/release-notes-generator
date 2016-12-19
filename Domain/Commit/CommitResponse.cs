using System.Collections.Generic;

namespace ReleaseNotesGenerator.Domain.Commit
{
    public class CommitResponse
    {
        public int Count { get; set; }

        public IList<Commit> Value { get; set; }
    }
}
