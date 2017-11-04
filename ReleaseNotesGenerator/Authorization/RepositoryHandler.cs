using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ReleaseNotesGenerator.Authorization
{
    public class RepositoryHandler : AuthorizationHandler<RepositoryModificationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RepositoryModificationRequirement requirement)
        {
            throw new NotImplementedException(); 
        }
    }

    public class RepositoryModificationRequirement : IAuthorizationRequirement
    {
    }
}