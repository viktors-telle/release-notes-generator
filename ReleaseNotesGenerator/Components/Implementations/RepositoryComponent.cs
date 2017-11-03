using System.Threading.Tasks;
using AutoMapper;
using ReleaseNotesGenerator.Components.Interfaces;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Components.Implementations
{
    public class RepositoryComponent : IRepositoryComponent
    {
        private readonly ReleaseNotesContext _context;
        private readonly IMapper _mapper;

        public RepositoryComponent(ReleaseNotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            _mapper.Map(repository, existingRepository);

            await _context.SaveChangesAsync();
            return existingRepository;
        }
    }
}