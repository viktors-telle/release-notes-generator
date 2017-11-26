using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ReleaseNotesGenerator.Components;

namespace ReleaseNotesGenerator.Middleware
{
    public class ApiAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _pathsWithoutAuthentication = { "/home", "/api/projects", "/swagger", "/api/projecttrackingtools" };

        public ApiAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IProjectComponent projectComponent)
        {
            if (context.Request.Path.HasValue)
            {
                if (_pathsWithoutAuthentication.Contains(context.Request.Path.Value.ToLowerInvariant()))
                {
                    await _next.Invoke(context);
                    return;
                }

                foreach (var pathWithoutAuthentication in _pathsWithoutAuthentication)
                {
                    if (context.Request.Path.Value.StartsWith(pathWithoutAuthentication,
                        StringComparison.InvariantCultureIgnoreCase))
                    {
                        await _next.Invoke(context);
                        return;
                    }
                }
            }

            var authHeaderParams = context.Request.Headers["Authorization"].ToString().Split(' ');
            if (authHeaderParams.Length != 2 || !await projectComponent.IsAuthenticated(authHeaderParams[0], authHeaderParams[1]))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }
    }
}
