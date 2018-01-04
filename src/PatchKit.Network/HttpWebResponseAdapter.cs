using System;
using System.IO;
using System.Net;

namespace PatchKit.Network
{
    /// <summary>
    /// Adapter of <see cref="System.Net.HttpWebRequest"/> for <see cref="IHttpResponse"/>.
    /// </summary>
    public class HttpWebResponseAdapter : IHttpResponse
    {
        private readonly HttpWebResponse _response;

        /// <inheritdoc />
        public HttpWebResponseAdapter(HttpWebResponse response)
        {
            _response = response;
        }

        /// <inheritdoc />
        public Stream ContentStream => _response.GetResponseStream();

        /// <inheritdoc />
        public string CharacterSet => _response.CharacterSet;

        /// <inheritdoc />
        public HttpStatusCode StatusCode => _response.StatusCode;

        /// <inheritdoc />
        public void Dispose()
        {
            ((IDisposable) _response)?.Dispose();
        }
    }
}