using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using FluentAssertions;
using PatchKit.Core;
using PatchKit.Core.Cancellation;
using Timeout = PatchKit.Core.Timeout;

namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="T:PatchKit.Network.IHttpClient" /> that uses .NET Framework classes.
    /// </summary>
    public class HttpClient : IHttpClient
    {
        /// <inheritdoc />
        public HttpResponse SendRequest(HttpGetRequest request, Timeout? timeout, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionIfNotValid(nameof(request));
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));

            return SendRequest(request.Address, timeout, httpWebRequest => { }, cancellationToken);
        }

        /// <inheritdoc />
        public HttpResponse SendRequest(HttpPostRequest request, Timeout? timeout, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionIfNotValid(nameof(request));
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));

            return SendRequest(request.Address, timeout, httpWebRequest =>
            {
                var content = request.Content.ToArray();

                httpWebRequest.Method = "Post";
                switch (request.ContentType)
                {
                    case HttpPostRequestContentType.ApplicationXWWWFormUrlEncoded:
                        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                        break;
                    case HttpPostRequestContentType.MultipartFormData:
                        httpWebRequest.ContentType = "multipart/form-data";
                        break;
                }

                httpWebRequest.ContentLength = content.Length;

                using (var requestDataStream = httpWebRequest.GetRequestStream())
                {
                    requestDataStream.Write(content.ToArray(), 0, content.Length);
                }
            }, cancellationToken);
        }

        private static HttpResponse SendRequest(HttpAddress address, Timeout? timeout,
            Action<HttpWebRequest> tweakMethodSpecific, CancellationToken cancellationToken)
        {
            //TODO: Support cancellation token here
            tweakMethodSpecific.Should().NotBeNull();

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(address.Uri);

            tweakMethodSpecific(httpWebRequest);

            if (timeout.HasValue)
            {
                httpWebRequest.Timeout = (int) Math.Min(int.MaxValue, timeout.Value.Value.TotalMilliseconds);
            }
            else
            {
                httpWebRequest.Timeout = System.Threading.Timeout.Infinite;
            }

            try
            {
                var httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                return GetResponse(httpWebResponse);
            }
            catch (WebException webException)
            {
                if (webException.Status == WebExceptionStatus.ProtocolError &&
                    webException.Response != null)
                {
                    var httpWebResponse = (HttpWebResponse) webException.Response;
                    return GetResponse(httpWebResponse);
                }

                throw;
            }
        }

        private static HttpResponse GetResponse(HttpWebResponse httpWebResponse)
        {
            using (var contentStream = httpWebResponse.GetResponseStream())
            {
                httpWebResponse.CharacterSet.Should().NotBeNull();
                contentStream.Should().NotBeNull();

                // ReSharper disable once AssignNullToNotNullAttribute
                var responseEncoding = Encoding.GetEncoding(httpWebResponse.CharacterSet);

                // ReSharper disable once AssignNullToNotNullAttribute
                using (var streamReader = new StreamReader(contentStream, responseEncoding))
                {
                    var body = streamReader.ReadToEnd();

                    return new HttpResponse(body, httpWebResponse.StatusCode);
                }
            }
        }
    }
}