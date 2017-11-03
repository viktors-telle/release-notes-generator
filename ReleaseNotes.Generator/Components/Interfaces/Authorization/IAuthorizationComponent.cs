﻿using System.Threading.Tasks;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Components.Interfaces.Authorization
{
    public interface IAuthorizationComponent
    {               
        Task<bool> IsAuthorizedToAdd(AuthorizationHeader authorizationHeader, int id);
    }
}
