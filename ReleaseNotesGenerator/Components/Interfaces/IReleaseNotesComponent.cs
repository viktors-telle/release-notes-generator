using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components.Interfaces
{
    public interface IReleaseNotesComponent
    {
        Task<string> Get(ReleaseNotesRequest releaseNotes);

        Task Save(string releaseNotes);
    }
}