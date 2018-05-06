using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using FluentAssertions;
using PatchKit.Core;
using Timeout = PatchKit.Core.Timeout;

namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="T:PatchKit.Network.IHttpClient" /> that uses .NET Framework classes.
    /// </summary>
    public class HttpClient : IHttpClient
    {
        private static readonly MethodInfo HttpWebRequestAddRangeHelper = typeof(WebHeaderCollection).GetMethod
            ("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <inheritdoc />
        public HttpResponse SendRequest(HttpGetRequest request, Timeout? timeout)
        {
            request.ThrowArgumentExceptionIfNotValid(nameof(request));
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));

            return SendRequest(request.Address, timeout, httpWebRequest =>
            {
                if (request.Range != null)
                {
                    SetRange(httpWebRequest, request.Range.Value);
                }
            });
        }

        /// <inheritdoc />
        public HttpResponse SendRequest(HttpPostRequest request, Timeout? timeout)
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
            });
        }

        private static HttpResponse SendRequest(Uri address, Timeout? timeout, Action<HttpWebRequest> tweakMethodSpecific)
        {
            tweakMethodSpecific.Should().NotBeNull();

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(address);

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

        private static void SetRange(HttpWebRequest httpWebRequest, BytesRange range)
        {
            range.IsValid().Should().BeTrue();

            var startText = range.Start.ToString();
            var endText = range.End.HasValue ? range.End.ToString() : string.Empty;

            HttpWebRequestAddRangeHelper.Invoke(httpWebRequest.Headers, new object[]
            {
                "Range",
                $"bytes={startText}-{endText}"
            });
        }
    }
}