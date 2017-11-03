using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Components.Interfaces
{
    public interface IRepositoryComponent
    {
        Task<Repository> GetById(int id);

        Task<int> Add(Repository repository);

        Task<Repository> Update(int id, Repository repository);
    }
}