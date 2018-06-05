using System;
using System.Net;
using System.Runtime.Serialization;

namespace PatchKit.Network
{
    [Serializable]
    public class HttpServerErrorException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public HttpServerErrorException()
        {
        }

        public HttpServerErrorException(string message) : base(message)
        {
        }

        public HttpServerErrorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HttpServerErrorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}