using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Core
{
    public class RepositoryComponent : IRepositoryComponent
    {
        private readonly ReleaseNotesContext _context;

        public RepositoryComponent(ReleaseNotesContext context)
        {
            _context = context;
        }

        public async Task<Repository> GetById(int id)
        {
            return await _context.Repositories.FindAsync(id);
        }

        public async Task<int> Add(Repository repository)
        {
            _context.Repositories.Add(repository);
            return await _context.SaveChangesAsync();
        }

        public async Task<Repository> Update(int id, Repository repository)
        {
            var existingRepository = await GetById(id);
            if (existingRepository == null)
            {
                return null;
            }

            existingRepository.Name = repository.Name;
            existingRepository.UserName = repository.UserName;
            existingRepository.Password = repository.Password;
            existingRepository.Url = repository.Url;
            existingRepository.ProjectId = repository.ProjectId;
            existingRepository.ProjectTrackingToolId = repository.ProjectTrackingToolId;
            existingRepository.RepositoryTypeId = repository.RepositoryTypeId;            

            await _context.SaveChangesAsync();
            return existingRepository;
        }
    }
}
