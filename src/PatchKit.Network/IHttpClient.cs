namespace PatchKit.Network
{
    /// <summary>
    /// HTTP client with ability to send requests.
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Sends GET request.
        /// </summary>
        /// <param name="getRequest">Request to send.</param>
        /// <returns>Response for sent request.</returns>
        IHttpResponse Get(HttpGetRequest getRequest);
    }
}