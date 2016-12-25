using System;
using System.Net;

namespace ReleaesNotesGenerator.Common.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public virtual HttpStatusCode StatusCode { get; }
    }
}