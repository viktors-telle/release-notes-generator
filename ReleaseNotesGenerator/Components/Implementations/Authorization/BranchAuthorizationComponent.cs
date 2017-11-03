using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReleaseNotesGenerator.Components.Interfaces.Authorization;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components.Implementations.Authorization
{
    public class BranchAuthorizationComponent : IBranchAuthorizationComponent
    {
        private readonly ReleaseNotesContext _context;

        public BranchAuthorizationComponent(ReleaseNotesContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAuthorizedToUpdate(AuthorizationHeader authorizationHeader, int repositoryId, int branchId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Name == authorizationHeader.Name 
                && p.ApiKey == authorizationHeader.ApiKey 
                && !p.IsDeactivated);
            if (project == null)
            {
                return false;
            }

            await _context.Entry(project).Collection(r => r.Repositories).LoadAsync();
            var repository = project.Repositories.FirstOrDefault(r => r.Id == repositoryId);
            if (repository == null)
            {
                return false;
            }

            await _context.Entry(repository).Collection(r => r.Branches).LoadAsync();
            return repository.Branches.Any(r => r.Id == branchId);                       
        }


        public async Task<bool> IsAuthorizedToGet(AuthorizationHeader authorizationHeader, int branchId)
        {
            var project = await _context.Projects.Include(p => p.Repositories)
                .ThenInclude(r => r.Branches)
                .FirstOrDefaultAsync(p => p.Name == authorizationHeader.Name
                                          && p.ApiKey == authorizationHeader.ApiKey
                                          && !p.IsDeactivated);

            return project.Repositories.Any(r => r.Branches.Any(b => b.Id == branchId));            
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