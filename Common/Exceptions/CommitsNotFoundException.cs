using System;
using System.Net;

namespace ReleaesNotesGenerator.Common.Exceptions
{
    public class CommitsNotFoundException : BaseException
    {
        public CommitsNotFoundException()
        {
        }

        public CommitsNotFoundException(string message) : base(message)
        {
        }

        public CommitsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}