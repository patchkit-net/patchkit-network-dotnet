using PatchKit.Core;
using PatchKit.Core.Cancellation;

namespace PatchKit.Network
{
    public delegate void OnBytesDownloadedHandler(byte[] bytes, int length);

    public interface IHttpDownloader
    {
        /// <summary>
        /// Downloads data from specified address.
        /// </summary>
        void Download(HttpAddress address, BytesRange? bytesRange,
            OnBytesDownloadedHandler onBytesDownloaded,
            Timeout? timeout,
            CancellationToken cancellationToken);
    }
}