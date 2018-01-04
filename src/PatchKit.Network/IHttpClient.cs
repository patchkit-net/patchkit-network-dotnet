namespace PatchKit.Network
{
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