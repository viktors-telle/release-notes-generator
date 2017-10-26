using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Domain.Commit;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class TfsRepositoryHandler : IRepositoryHandler
    {
        public Task<IList<Commit>> GetCommits(CommitQuery query)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit> commits)
        {
            throw new System.NotImplementedException();
        }
    }
}