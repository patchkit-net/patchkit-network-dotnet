namespace PatchKit.Network
{
    /// <summary>
    /// Describes HTTP GET request.
    /// </summary>
    public class HttpGetRequest : BaseHttpRequest
    {
        /// <summary>
        /// Requested bytes range.
        /// If set to <c>null</c> range is not specified.
        /// </summary>
        public BytesRange? Range { get; set; }
    }
}