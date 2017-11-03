using System;
using System.Net;

namespace ReleaseNotes.Generator.Attributes
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
