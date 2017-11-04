using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Components.Interfaces
{
    public interface IProjectComponent
    {
        Task<bool> IsAuthenticated(string name, string apiKey);

        Task<Project> GetById(int id);

        Task<IEnumerable<Project>> GetProjects();

        Task<int> Add(Project project);

        Task<Project> Update(int id, Project project);
    }
}