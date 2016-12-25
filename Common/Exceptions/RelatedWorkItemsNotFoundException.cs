using System;
using System.Net;

namespace ReleaesNotesGenerator.Common.Exceptions
{
    public class RelatedWorkItemsNotFoundException : BaseException
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

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}