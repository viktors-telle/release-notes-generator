using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain.Commit;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.RepositoryHandlers
{
    public interface IRepositoryHandler
    {
        Task<IList<Commit>> GetCommits(CommitQuery query);

        Task<IEnumerable<Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit> commits);
    }
}