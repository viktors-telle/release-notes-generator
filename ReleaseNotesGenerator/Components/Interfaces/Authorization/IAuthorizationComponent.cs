using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components.Interfaces.Authorization
{
    public interface IAuthorizationComponent
    {               
        Task<bool> IsAuthorizedToAdd(AuthorizationHeader authorizationHeader, int id);
    }
}
