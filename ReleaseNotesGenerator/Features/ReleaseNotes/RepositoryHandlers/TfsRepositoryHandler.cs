using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Features.ReleaseNotes.Commit;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.RepositoryHandlers
{
    public class TfsRepositoryHandler : IRepositoryHandler
    {
        public Task<List<Commit.Commit>> GetCommits(CommitQuery query)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Commit.Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit.Commit> commits)
        {
            throw new System.NotImplementedException();
        }
    }
}