using System.Threading.Tasks;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public interface IProjectTrackingToolComponent
    {
        Task<ProjectTrackingTool> GetById(int id);

        Task<int> Add(ProjectTrackingTool projectTrackingTool);

        Task<ProjectTrackingTool> Update(int id, ProjectTrackingTool projectTrackingTool);
    }
}