using System.Threading.Tasks;
using AutoMapper;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Dal;
using ReleaseNotes.Generator.Domain;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class BranchComponent : IBranchComponent
    {
        private readonly ReleaseNotesContext _context;
        private readonly IMapper _mapper;

        public BranchComponent(ReleaseNotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Branch> GetById(int id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public async Task<int> Add(Branch branch)
        {
            _context.Branches.Add(branch);
            return await _context.SaveChangesAsync();
        }

        public async Task<Branch> Update(int id, Branch branch)
        {
            var existingBranch = await GetById(id);
            if (existingBranch == null)
            {
                return null;
            }

            _mapper.Map(branch, existingBranch);

            await _context.SaveChangesAsync();
            return existingBranch;
        }
    }
}