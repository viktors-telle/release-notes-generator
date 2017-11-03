using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Api
{    
    public abstract class BaseController : Controller
    {
        protected AuthorizationHeader GetAuthorizationParameters(IHeaderDictionary headers)
        {
            var authHeaderParams = headers["Authorization"].ToString().Split(' ');
            return new AuthorizationHeader
            {
                Name = authHeaderParams[0],
                ApiKey = authHeaderParams[1]
            };
        }
    }
}
