using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Features.ReleaseNotes.Commit;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.RepositoryHandlers
{
    public interface IRepositoryHandler
    {
        Task<IList<Commit.Commit>> GetCommits(CommitQuery query);

        Task<IEnumerable<Commit.Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit.Commit> commits);
    }
}