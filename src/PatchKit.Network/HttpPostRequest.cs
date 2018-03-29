namespace PatchKit.Network
{
    /// <summary>
    /// Describes HTTP POST request.
    /// </summary>
    public class HttpPostRequest : BaseHttpRequest
    {
        /// <summary>
        /// Request body.
        /// </summary>
        public string Body { get; set; }
    }
}