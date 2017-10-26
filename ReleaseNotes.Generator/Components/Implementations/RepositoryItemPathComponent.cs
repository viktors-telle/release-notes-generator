using System.Threading.Tasks;
using AutoMapper;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Dal;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class RepositoryItemPathComponent : IRepositoryItemPathComponent
    {
        private readonly ReleaseNotesContext _context;
        private readonly IMapper _mapper;

        public RepositoryItemPathComponent(ReleaseNotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RepositoryItemPath> GetById(int id)
        {
            return await _context.RepositoryItemPaths.FindAsync(id);
        }

        public async Task<int> Add(RepositoryItemPath repositoryItemPath)
        {
            _context.RepositoryItemPaths.Add(repositoryItemPath);
            return await _context.SaveChangesAsync();
        }

        public async Task<RepositoryItemPath> Update(int id, RepositoryItemPath repositoryItemPath)
        {
            var existingRepositoryItemPath = await GetById(id);
            if (existingRepositoryItemPath == null)
            {
                return null;
            }

            _mapper.Map(repositoryItemPath, existingRepositoryItemPath);

            await _context.SaveChangesAsync();
            return existingRepositoryItemPath;
        }
    }
}