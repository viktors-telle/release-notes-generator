using ReleaseNotesGenerator.Common.Models;
using ReleaseNotesGenerator.Dal;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Core
{
    public class ReleaseNotesComponent : IReleaseNotesComponent
    {
        private readonly ReleaseNotesContext _context;

        public ReleaseNotesComponent(ReleaseNotesContext context)
        {
            _context = context;
        }

        public async Task<string> Get(ReleaseNotesRequest releaseNotes)
        {
            var repository = await _context.Repositories.FindAsync(releaseNotes.RepositoryId);


            return string.Empty;
        }
    }
}
