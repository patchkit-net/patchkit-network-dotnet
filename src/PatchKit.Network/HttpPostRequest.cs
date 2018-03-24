using System;

namespace PatchKit.Network
{
    /// <summary>
    /// Describes HTTP POST request.
    /// </summary>
    public class HttpPostRequest : BaseHttpRequest
    {
        /// <summary>
        /// Request query to be sent in body.
        /// </summary>
        public string Query { get; set; }
    }
}