using System;
using System.Net;
using System.Runtime.Serialization;
using ReleaseNotes.Generator.Attributes;

namespace ReleaseNotes.Generator.Exceptions
{
    [HttpStatusCode(HttpStatusCode.BadRequest)]
    public class ProjectIsDeactivatedException : BaseException
    {
        public ProjectIsDeactivatedException()
        {
        }

        public ProjectIsDeactivatedException(string message) : base(message)
        {
        }

        public ProjectIsDeactivatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProjectIsDeactivatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}