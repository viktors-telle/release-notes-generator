using ReleaseNotesGenerator.Common.Models;
using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Core
{
    public interface IReleaseNotesComponent
    {
        Task<string> Get(ReleaseNotesRequest releaseNotes);
    }
}