using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Features.Projects
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