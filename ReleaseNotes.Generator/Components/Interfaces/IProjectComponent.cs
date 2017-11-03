using System.Threading.Tasks;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Components.Interfaces
{
    public interface IProjectComponent
    {
        Task<bool> IsAuthenticated(string name, string apiKey);

        Task<Project> GetById(int id);

        Task<int> Add(Project project);

        Task<Project> Update(int id, Project project);
    }
}