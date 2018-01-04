using System;

namespace PatchKit.Network
{
    public class HttpGetRequest
    {
        /// <summary>
        /// Target request address.
        /// </summary>
        public Uri Address { get; set; }
        
        /// <summary>
        /// Request timeout in miliseconds.
        /// </summary>
        public int Timeout { get; set; }
        
        /// <summary>
        /// Requested bytes range.
        /// If set to <c>null</c> range is not specified.
        /// </summary>
        public BytesRange? Range { get; set; }
    }
}