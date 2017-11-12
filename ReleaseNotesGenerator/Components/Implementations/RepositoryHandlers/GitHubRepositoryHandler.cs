using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Components.Interfaces;
using ReleaseNotesGenerator.Domain.Commit;

namespace ReleaseNotesGenerator.Components.Implementations.RepositoryHandlers
{
    public class GitHubRepositoryHandler : IRepositoryHandler
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