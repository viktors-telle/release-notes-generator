using System;

namespace ReleaesNotesGenerator.Common.Exceptions
{
    public class RelatedWorkItemsNotFoundException : Exception
    {
        public RelatedWorkItemsNotFoundException()
        {
        }

        public RelatedWorkItemsNotFoundException(string message) : base(message)
        {
        }

        public RelatedWorkItemsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}