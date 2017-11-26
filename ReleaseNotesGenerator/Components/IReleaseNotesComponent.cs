using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components
{
    public interface IReleaseNotesComponent
    {
        Task<string> Get(ReleaseNotesRequest request);
    }
}