using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Features.ReleaseNotes;

namespace ReleaseNotesGenerator.Features.SourceCodeRepositories
{
    public interface IRepositoryComponent
    {
        Task<Repository> GetById(int id);

        Task<int> Add(Repository repository);

        Task<Repository> Update(int id, Repository repository);

        Task<IEnumerable<Repository>> GetRepositories();

        Task<IEnumerable<ReleaseNote>> GetRepositoryReleaseNotes(int repositoryId);
    }
}