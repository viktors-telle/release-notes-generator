using System.Threading.Tasks;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Components.Interfaces.Authorization
{
    public interface IRepositoryItemPathAuthorizationComponent : IAuthorizationComponent
    {
        Task<bool> IsAuthorizedToGetOrUpdate(AuthorizationHeader authorizationHeader, int repositoryItemPathId);        
    }
}