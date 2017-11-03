using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReleaseNotesGenerator.Components.Interfaces.Authorization;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components.Implementations.Authorization
{
    public class RepositoryItemPathAuthorizationComponent : IRepositoryItemPathAuthorizationComponent
    {
        private readonly ReleaseNotesContext _context;

        public RepositoryItemPathAuthorizationComponent(ReleaseNotesContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAuthorizedToGetOrUpdate(AuthorizationHeader authorizationHeader, int repositoryItemPathId)
        {
            var project = await _context.Projects
                .Include(p => p.Repositories)
                .ThenInclude(r => r.Branches)
                .ThenInclude(b => b.RepositoryItemPaths)
                .FirstOrDefaultAsync(p => p.Name == authorizationHeader.Name 
                    && p.ApiKey == authorizationHeader.ApiKey 
                    && !p.IsDeactivated);

            if (project == null)
            {
                return false;
            }

            return
                project.Repositories.Any(
                    r => r.Branches.Any(b => b.RepositoryItemPaths.Any(rip => rip.Id == repositoryItemPathId)));
        }

        public async Task<bool> IsAuthorizedToAdd(AuthorizationHeader authorizationHeader, int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Name == authorizationHeader.Name 
                && p.ApiKey == authorizationHeader.ApiKey 
                && !p.IsDeactivated);

            if (project == null)
            {
                return false;
            }

            await _context.Entry(project).Collection(r => r.Repositories).LoadAsync();
            return project.Repositories.Any(r => r.Id == id);            
        }
    }
}