using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components.Interfaces.Authorization
{
    public interface IRepositoryAuthorizationComponent : IAuthorizationComponent
    {
        Task<bool> IsAuthorizedToGetOrUpdate(AuthorizationHeader authorizationHeader, int repositoryId);
    }
}