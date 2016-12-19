using ReleaseNotesGenerator.Domain.Commit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.RepositoryHandlers.Core
{
    public class TfsRepositoryHandler : IRepositoryHandler
    {
        public Task<IList<Commit>> GetCommits(CommitQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
