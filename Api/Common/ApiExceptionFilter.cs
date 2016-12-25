using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReleaesNotesGenerator.Common.Exceptions;

namespace ReleaseNotesGenerator.Common
{
    // TODO: Add this filter globally.
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Console.WriteLine($"Exception occured");

            
            if (context.Exception is BaseException)
            {
                var ex = context.Exception as BaseException;
                context.HttpContext.Response.StatusCode = (int)ex.StatusCode;
            }

            context.Result = new JsonResult(context.Exception);

            base.OnException(context);
        }
    }
}