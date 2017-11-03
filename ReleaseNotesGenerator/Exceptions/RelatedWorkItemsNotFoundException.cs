using System;
using System.Net;
using System.Runtime.Serialization;
using ReleaseNotesGenerator.Attributes;

namespace ReleaseNotesGenerator.Exceptions
{
    [HttpStatusCode(HttpStatusCode.BadRequest)]
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

        protected RelatedWorkItemsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}