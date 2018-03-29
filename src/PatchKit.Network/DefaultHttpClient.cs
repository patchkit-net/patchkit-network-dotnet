using System;
using System.Net;
using System.Text;
using System.Reflection;

namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Default implementation of <see cref="T:PatchKit.Network.IHttpClient" />.
    /// </summary>
    public class DefaultHttpClient : IHttpClient
    {
        private static readonly MethodInfo HttpWebRequestAddRangeHelper = typeof(WebHeaderCollection).GetMethod
            ("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <inheritdoc />
        public IHttpResponse Get(HttpGetRequest getRequest)
        {
            return Request(getRequest, httpWebRequest => {
                if (getRequest.Range != null)
                {
                    SetRange(httpWebRequest, getRequest.Range.Value);
                }
            });
        }

        /// <inheritdoc />
        public IHttpResponse Post(HttpPostRequest postRequest)
        {
            return Request(postRequest, httpWebRequest => {
                var requestData = Encoding.ASCII.GetBytes(postRequest.Body);

                httpWebRequest.Method = "Post";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.ContentLength = requestData.Length;

                using (var requestDataStream = httpWebRequest.GetRequestStream())
                {
                    requestDataStream.Write(requestData, 0, requestData.Length);
                }
            });
        }

        private IHttpResponse Request(BaseHttpRequest request, Action<HttpWebRequest> tweakMethodSpecific)
        {
            if (request.Address.Scheme != Uri.UriSchemeHttp && request.Address.Scheme != Uri.UriSchemeHttps)
            {
                throw new NotSupportedException("Request address is not pointing to HTTP/HTTPS resource.");
            }

            if (tweakMethodSpecific == null)
            {
                throw new ArgumentNullException(nameof(tweakMethodSpecific));
            }

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(request.Address);

            tweakMethodSpecific(httpWebRequest);

            httpWebRequest.Timeout = request.Timeout;

            HttpWebResponse httpWebResponse;
            
            try
            {
                httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
            }
            catch (WebException webException)
            {
                if (webException.Status == WebExceptionStatus.ProtocolError &&
                    webException.Response != null)
                {
                    httpWebResponse = (HttpWebResponse) webException.Response;
                }
                else
                {
                    throw;
                }
            }
            
            return new HttpWebResponseAdapter(httpWebResponse);
        }

        private void SetRange(HttpWebRequest httpWebRequest, BytesRange range)
        {
            if (range.Start < 0L)
            {
                throw new ArgumentOutOfRangeException(nameof(range.Start));
            }
            
            if (range.End >= 0L && range.End < range.Start)
            {
                throw new ArgumentOutOfRangeException(nameof(range.End));
            }

            var startText = range.Start.ToString();
            var endText = range.End >= 0L ? range.End.ToString() : string.Empty;
            
            HttpWebRequestAddRangeHelper.Invoke(httpWebRequest.Headers, new object[]
            {
                "Range", 
                $"bytes={startText}-{endText}"
            });
        }
    }
}