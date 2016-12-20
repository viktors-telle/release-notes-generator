using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain.Commit;

namespace ReleaseNotesGenerator.Core.RepositoryHandlers
{
    public interface IRepositoryHandler
    {
        Task<IList<Commit>> GetCommits(CommitQuery query);
    }  
}