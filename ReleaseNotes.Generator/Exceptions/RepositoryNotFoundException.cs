using System;
using System.Net;
using System.Runtime.Serialization;
using ReleaseNotes.Generator.Attributes;

namespace ReleaseNotes.Generator.Exceptions
{
    [HttpStatusCode(HttpStatusCode.BadRequest)]
    public class RepositoryNotFoundException : BaseException
    {
        public RepositoryNotFoundException()
        {
        }

        public RepositoryNotFoundException(string message) : base(message)
        {
        }

        public RepositoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepositoryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}