using System;

namespace PatchKit.Network
{
    /// <summary>
    /// A base http request
    /// </summary>
    public abstract class BaseHttpRequest
    {
        /// <summary>
        /// Target request address.
        /// </summary>
        public Uri Address { get; set; }
        
        /// <summary>
        /// Request timeout in miliseconds.
        /// </summary>
        public int Timeout { get; set; }
    }
}