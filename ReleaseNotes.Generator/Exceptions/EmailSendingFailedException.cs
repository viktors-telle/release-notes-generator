using System;
using System.Net;
using System.Runtime.Serialization;
using ReleaseNotes.Generator.Attributes;

namespace ReleaseNotes.Generator.Exceptions
{
    [HttpStatusCode(HttpStatusCode.BadRequest)]
    public class EmailSendingFailedException : BaseException
    {
        public EmailSendingFailedException()
        {
        }

        public EmailSendingFailedException(string message) : base(message)
        {
        }

        public EmailSendingFailedException(string message, Exception innerException) : base(message, innerException)
        {            
        }

        protected EmailSendingFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}