using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Core
{
    public class ProjectComponent : IProjectComponent
    {
        private readonly ReleaseNotesContext _context;

        public ProjectComponent(ReleaseNotesContext context)
        {
            _context = context;
        }

        public async Task<Project> GetById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<int> Add(Project project)
        {
            _context.Projects.Add(project);
            return await _context.SaveChangesAsync();
        }

        public async Task<Project> Update(int id, Project project)
        {
            var existingProject = await GetById(id);
            if (existingProject == null)
            {
                return null;
            }

            existingProject.Name = project.Name;
            existingProject.ApiKey = project.ApiKey;            
            existingProject.IsDeactivated = project.IsDeactivated;            
            await _context.SaveChangesAsync();
            return existingProject;
        }
    }
}
