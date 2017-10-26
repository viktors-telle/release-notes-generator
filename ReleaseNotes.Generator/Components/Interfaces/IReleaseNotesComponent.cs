using System.Threading.Tasks;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator
{
    public interface IReleaseNotesComponent
    {
        Task<string> Get(ReleaseNotesRequest releaseNotes);
    }
}