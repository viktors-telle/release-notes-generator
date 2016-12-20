using System;

namespace ReleaesNotesGenerator.Common.Exceptions
{
    public class CommitsNotFoundException : Exception
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
    }
}