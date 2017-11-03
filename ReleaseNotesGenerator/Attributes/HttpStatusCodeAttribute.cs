using System;
using System.Net;

namespace ReleaseNotesGenerator.Attributes
{
    public class HttpStatusCodeAttribute : Attribute
    {
        public HttpStatusCode HttpStatusCode { get; }

        public HttpStatusCodeAttribute(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
