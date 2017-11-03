using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReleaseNotes.Generator.Components.Interfaces.Authorization;
using ReleaseNotes.Generator.Dal;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Components.Implementations.Authorization
{
    public class RepositoryAuthorizationComponent : IRepositoryAuthorizationComponent
    {
        private readonly ReleaseNotesContext _context;

        public RepositoryAuthorizationComponent(ReleaseNotesContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAuthorizedToGetOrUpdate(AuthorizationHeader authorizationHeader, int id)
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

        public async Task<bool> IsAuthorizedToAdd(AuthorizationHeader authorizationHeader, int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Name == authorizationHeader.Name 
                && p.ApiKey == authorizationHeader.ApiKey 
                && !p.IsDeactivated && p.Id == id);
            if (project == null)
            {
                return false;
            }

            return true;
        }
    }
}