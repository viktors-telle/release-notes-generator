using System;
using System.Net;
using System.Runtime.Serialization;
using ReleaseNotes.Generator.Attributes;

namespace ReleaseNotes.Generator.Exceptions
{
    [HttpStatusCode(HttpStatusCode.BadRequest)]
    public class RepositoryItemPathDoesNotExistException : BaseException
    {
        public RepositoryItemPathDoesNotExistException()
        {
        }

        public RepositoryItemPathDoesNotExistException(string message) : base(message)
        {
        }

        public RepositoryItemPathDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepositoryItemPathDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}