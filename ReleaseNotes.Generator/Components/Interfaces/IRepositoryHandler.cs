using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotes.Generator.Domain.Commit;

namespace ReleaseNotes.Generator.Components.Interfaces
{
    public interface IRepositoryHandler
    {
        Task<IList<Commit>> GetCommits(CommitQuery query);

        Task<IEnumerable<Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit> commits);
    }
}