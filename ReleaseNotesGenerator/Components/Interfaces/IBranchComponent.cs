using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Components.Interfaces
{
    public interface IBranchComponent
    {
        Task<Branch> GetById(int id);

        Task<int> Add(Branch branch);

        Task<Branch> Update(int id, Branch branch);
    }
}