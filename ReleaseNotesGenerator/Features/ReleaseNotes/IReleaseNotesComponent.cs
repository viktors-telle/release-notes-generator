using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Features.ReleaseNotes
{
    public interface IReleaseNotesComponent
    {
        Task<string> Get(ReleaseNotesRequest request);
    }
}