using System.Threading.Tasks;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Components.Interfaces.Authorization
{
    public interface IBranchAuthorizationComponent : IAuthorizationComponent
    {
        Task<bool> IsAuthorizedToUpdate(AuthorizationHeader authorizationHeader, int repositoryId, int branchId);

        Task<bool> IsAuthorizedToGet(AuthorizationHeader authorizationHeader, int branchId);
    }
}