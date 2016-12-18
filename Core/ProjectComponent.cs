using AutoMapper;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Core
{
    public class ProjectComponent : IProjectComponent
    {
        private readonly ReleaseNotesContext _context;
        private readonly IMapper _mapper;

        public ProjectComponent(ReleaseNotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            _mapper.Map(project, existingProject);
            await _context.SaveChangesAsync();
            return existingProject;
        }
    }
}
