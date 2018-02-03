using System.Threading.Tasks;
using AutoMapper;
using ReleaseNotesGenerator.Dal;

namespace ReleaseNotesGenerator.Features.ProjectTrackingTools
{
    public class ProjectTrackingToolComponent : IProjectTrackingToolComponent
    {
        private readonly ReleaseNotesContext _context;
        private readonly IMapper _mapper;

        public ProjectTrackingToolComponent(ReleaseNotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectTrackingTool> GetById(int id)
        {
            return await _context.ProjectTrackingTools.FindAsync(id);
        }

        public async Task<int> Add(ProjectTrackingTool projectTrackingTool)
        {
            _context.ProjectTrackingTools.Add(projectTrackingTool);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProjectTrackingTool> Update(int id, ProjectTrackingTool projectTrackingTool)
        {
            var existingTrackingTool = await GetById(id);
            if (existingTrackingTool == null)
            {
                return null;
            }

            _mapper.Map(projectTrackingTool, existingTrackingTool);

            await _context.SaveChangesAsync();
            return existingTrackingTool;
        }
    }
}