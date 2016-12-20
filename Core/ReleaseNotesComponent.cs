using ReleaseNotesGenerator.Common.Models;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain.Commit;
using System.Linq;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Core.RepositoryHandlers;

namespace ReleaseNotesGenerator.Core
{
    public class ReleaseNotesComponent : IReleaseNotesComponent
    {
        private readonly ReleaseNotesContext _context;

        public ReleaseNotesComponent(ReleaseNotesContext context)
        {
            _context = context;
        }

        public async Task<string> Get(ReleaseNotesRequest releaseNotes)
        {
            var repository = await _context.Repositories.FindAsync(releaseNotes.RepositoryId);
            await _context.Entry(repository).Collection(r => r.Branches).LoadAsync();
            await _context.Entry(repository).Reference(r => r.RepositoryType).LoadAsync();

            var branch = repository.Branches.First(b => b.Name == releaseNotes.BranchName);
            var repositoryHandler = RepositoryFactory<IRepositoryHandler>.Create(repository.RepositoryType.Type);
            var commits = await repositoryHandler.GetCommits(new CommitQuery()
            {
                Url = repository.Url,
                BranchName = branch.Name,
                LastCommitId = branch.LastCommitId,
                RepositoryName = repository.Name,
                AccessToken = repository.AccessToken
            });

            return string.Empty;
        }
    }
}
