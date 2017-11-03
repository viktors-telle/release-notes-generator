using System;
using System.Net;
using System.Runtime.Serialization;
using ReleaseNotes.Generator.Attributes;

namespace ReleaseNotes.Generator.Exceptions
{
    [HttpStatusCode(HttpStatusCode.BadRequest)]
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

        protected CommitsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}