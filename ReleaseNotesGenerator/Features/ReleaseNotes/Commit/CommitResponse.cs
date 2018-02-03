using System.Collections.Generic;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.Commit
{
    public class CommitResponse
    {
        public int Count { get; set; }

        public IList<Commit> Value { get; set; }
    }
}