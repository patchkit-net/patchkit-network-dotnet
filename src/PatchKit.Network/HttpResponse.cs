using System;
using System.Net;
using PatchKit.Core;

namespace PatchKit.Network
{
    /// <summary>
    /// HTTP response.
    /// </summary>
    public struct HttpResponse : IValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponse"/> struct.
        /// </summary>
        public HttpResponse(string body, HttpStatusCode statusCode)
        {
            Body = body;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Response body.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// Response status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <inheritdoc />
        public string ValidationError
        {
            get
            {
                if (Body == null)
                {
                    return "Body cannot be null.";
                }

                if (!Enum.IsDefined(typeof(HttpStatusCode), StatusCode))
                {
                    return "StatusCode must be defined enum value.";
                }

                return null;
            }
        }
    }
}