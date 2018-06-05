using System;
using System.Net;
using System.Reflection;
using FluentAssertions;
using PatchKit.Core;
using PatchKit.Core.Cancellation;

namespace PatchKit.Network
{
    public class HttpDownloader : IHttpDownloader
    {
        private readonly int _bufferSize; // 1024 default

        private static readonly MethodInfo HttpWebRequestAddRangeHelper = typeof(WebHeaderCollection).GetMethod
            ("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);

        public HttpDownloader(int bufferSize)
        {
            _bufferSize = bufferSize;

            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            ServicePointManager.DefaultConnectionLimit = 65535;
        }

        public void Download(HttpAddress address, BytesRange? bytesRange,
            OnBytesDownloadedHandler onBytesDownloaded,
            Timeout? timeout,
            CancellationToken cancellationToken)
        {
            address.ThrowArgumentExceptionIfNotValid(nameof(address));
            bytesRange?.ThrowArgumentExceptionIfNotValid(nameof(bytesRange));
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(address.Uri);

            if (bytesRange.HasValue)
            {
                SetRange(httpWebRequest, bytesRange.Value);
            }

            if (timeout.HasValue)
            {
                httpWebRequest.Timeout = (int) Math.Min(int.MaxValue, timeout.Value.Value.TotalMilliseconds);
            }
            else
            {
                httpWebRequest.Timeout = System.Threading.Timeout.Infinite;
            }

            var response = (HttpWebResponse) httpWebRequest.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                stream.Should().NotBeNull();

                var buffer = new byte[_bufferSize];
                int bufferRead;
                // ReSharper disable once PossibleNullReferenceException
                while ((bufferRead = stream.Read(buffer, 0, _bufferSize)) > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    onBytesDownloaded(buffer, bufferRead);
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