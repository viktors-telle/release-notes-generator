using System.Threading.Tasks;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Components.Interfaces.Authorization
{
    public interface IRepositoryAuthorizationComponent : IAuthorizationComponent
    {
        Task<bool> IsAuthorizedToGetOrUpdate(AuthorizationHeader authorizationHeader, int repositoryId);
    }
}