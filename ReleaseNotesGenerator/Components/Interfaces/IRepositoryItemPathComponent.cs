using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Components.Interfaces
{
    public interface IRepositoryItemPathComponent
    {
        Task<RepositoryItemPath> GetById(int id);

        Task<int> Add(RepositoryItemPath repositoryItemPath);

        Task<RepositoryItemPath> Update(int id, RepositoryItemPath repositoryItemPath);
    }
}