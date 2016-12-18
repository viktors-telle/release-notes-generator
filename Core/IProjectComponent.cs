using ReleaseNotesGenerator.Domain;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Core
{
    public interface IProjectComponent
    {
        Task<Project> GetById(int id);

        Task<int> Add(Project project);

        Task<Project> Update(int id, Project project);
    }
}