using System.Threading.Tasks;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Components.Interfaces
{
    public interface IRepositoryComponent
    {
        Task<Repository> GetById(int id);

        Task<int> Add(Repository repository);

        Task<Repository> Update(int id, Repository repository);
    }
}