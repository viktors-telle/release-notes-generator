using ReleaseNotesGenerator.Domain;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Core
{
    public interface IRepositoryComponent
    {
        Task<Repository> GetById(int id);

        Task<int> Add(Repository repository);

        Task<Repository> Update(int id, Repository repository);
    }
}