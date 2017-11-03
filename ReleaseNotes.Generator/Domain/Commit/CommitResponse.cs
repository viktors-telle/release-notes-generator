using System.Collections.Generic;

namespace ReleaseNotes.Generator.Domain.Commit
{
    public class CommitResponse
    {
        public int Count { get; set; }

        public IList<Commit> Value { get; set; }
    }
}