using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain.Commit;

namespace ReleaseNotesGenerator.Components.RepositoryHandlers
{
    public class GitHubRepositoryHandler : IRepositoryHandler
    {
        public Task<IList<Commit>> GetCommits(CommitQuery query)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit> commits)
        {
            return await Task.FromResult(new List<Commit>());
        }
    }
}