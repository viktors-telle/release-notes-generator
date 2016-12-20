using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain.Commit;

namespace ReleaseNotesGenerator.Core.RepositoryHandlers
{
    public class TfsRepositoryHandler : IRepositoryHandler
    {
        public Task<IList<Commit>> GetCommits(CommitQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
