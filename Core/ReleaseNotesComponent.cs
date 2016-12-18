using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Core
{
    public class ReleaseNotesComponent : IReleaseNotesComponent
    {
        public async Task<string> Get()
        {
            return string.Empty;
        }
    }
}
