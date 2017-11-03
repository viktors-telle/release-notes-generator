using System.Threading.Tasks;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Components.Interfaces
{
    public interface IBranchComponent
    {
        Task<Branch> GetById(int id);

        Task<int> Add(Branch branch);

        Task<Branch> Update(int id, Branch branch);
    }
}