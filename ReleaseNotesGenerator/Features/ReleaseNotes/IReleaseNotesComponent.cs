using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Features.ReleaseNotes
{
    public interface IReleaseNotesComponent
    {
        Task<string> Get(ReleaseNotesRequest request);
    }
}