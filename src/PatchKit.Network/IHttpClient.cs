using PatchKit.Core;
using PatchKit.Core.Cancellation;

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
        /// <param name="request">Request to send.</param>
        /// <param name="timeout">Timeout. If set to <c>null</c> then timeout is diabled.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response for sent request.</returns>
        HttpResponse SendRequest(HttpGetRequest request, Timeout? timeout, CancellationToken cancellationToken);

        /// <summary>
        /// Sends POST request.
        /// </summary>
        /// <param name="request">Request to send.</param>
        /// <param name="timeout">Timeout. If set to <c>null</c> then timeout is diabled.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response for sent request.</returns>
        HttpResponse SendRequest(HttpPostRequest request, Timeout? timeout, CancellationToken cancellationToken);
    }
}