using System;
using System.Net;
using System.Runtime.Serialization;
using ReleaseNotesGenerator.Attributes;

namespace ReleaseNotesGenerator.Exceptions
{
    [HttpStatusCode(HttpStatusCode.BadRequest)]
    public class ProjectNotExistsOrIsDeactivatedException : BaseException
    {
        public ProjectNotExistsOrIsDeactivatedException()
        {
        }

        public ProjectNotExistsOrIsDeactivatedException(string message) : base(message)
        {
        }

        public ProjectNotExistsOrIsDeactivatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProjectNotExistsOrIsDeactivatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}