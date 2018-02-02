using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Features.Projects
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

        public async Task<bool> IsAuthenticated(string name, string apiKey)
        {
            return await _context.Projects.AnyAsync(p => p.Name == name && p.ApiKey == apiKey && !p.IsDeactivated);
        }

        public async Task<Project> GetById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
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