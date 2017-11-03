using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components.Interfaces.Authorization
{
    public interface IBranchAuthorizationComponent : IAuthorizationComponent
    {
        Task<bool> IsAuthorizedToUpdate(AuthorizationHeader authorizationHeader, int repositoryId, int branchId);

        Task<bool> IsAuthorizedToGet(AuthorizationHeader authorizationHeader, int branchId);
    }
}