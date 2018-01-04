using System;
using System.Net;
using System.Reflection;

namespace PatchKit.Network
{
    public class DefaultHttpClient : IHttpClient
    {
        private static readonly MethodInfo HttpWebRequestAddRangeHelper = typeof(WebHeaderCollection).GetMethod
            ("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <inheritdoc />
        public IHttpResponse Get(HttpGetRequest getRequest)
        {
            if (getRequest.Address.Scheme != Uri.UriSchemeHttp && getRequest.Address.Scheme != Uri.UriSchemeHttps)
            {
                throw new NotSupportedException("Request address is not pointing to HTTP/HTTPS resource.");
            }

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(getRequest.Address);

            if (getRequest.Range != null)
            {
                SetRange(httpWebRequest, getRequest.Range.Value);
            }

            httpWebRequest.Timeout = getRequest.Timeout;

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
            var endText = range.End > 0L ? range.End.ToString() : string.Empty;
            
            HttpWebRequestAddRangeHelper.Invoke(httpWebRequest.Headers, new object[]
            {
                "Range", 
                $"bytes={startText}-{endText}"
            });
        }
    }
}