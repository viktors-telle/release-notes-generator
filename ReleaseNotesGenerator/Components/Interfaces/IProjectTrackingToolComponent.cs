using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Components.Interfaces
{
    public interface IProjectTrackingToolComponent
    {
        Task<ProjectTrackingTool> GetById(int id);

        Task<int> Add(ProjectTrackingTool projectTrackingTool);

        Task<ProjectTrackingTool> Update(int id, ProjectTrackingTool projectTrackingTool);
    }
}