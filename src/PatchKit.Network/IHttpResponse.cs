using System;
using System.IO;
using System.Net;

namespace PatchKit.Network
{
    /// <summary>
    /// Provides a HTTP response.
    /// </summary>
    public interface IHttpResponse : IDisposable
    {
        /// <summary>
        /// Gets the stream that is used to read the body of the response from the server.
        /// </summary>
        Stream ContentStream { get; }
        
        /// <summary>
        /// Gets the character set of the response.
        /// </summary>
        string CharacterSet { get; }
        
        /// <summary>
        /// Gets the status of the response.
        /// </summary>
        HttpStatusCode StatusCode { get; }
    }
}