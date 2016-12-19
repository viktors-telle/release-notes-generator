using ReleaseNotesGenerator.Domain.Commit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.RepositoryHandlers.Core
{
    public interface IRepositoryHandler
    {
        Task<IList<Commit>> GetCommits(CommitQuery query);
    }  
}