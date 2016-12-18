using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Controllers
{
    [Route("api/[controller]")]
    public class RepositoriesController : Controller
    {
        private readonly ReleaseNotesContext _context;

        public RepositoriesController(ReleaseNotesContext context)
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
