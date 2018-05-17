using System;
using System.Net;
using PatchKit.Core;

namespace PatchKit.Network
{
    public struct HttpAddress : IValidatable
    {
        public Uri Uri { get; }

        public HttpAddress(Uri uri)
        {
            Uri = uri;
        }

        public string ValidationError
        {
            get
            {
                if (Uri == null)
                {
                    return $"{nameof(Uri)} cannot be null.";
                }

                if (Uri.Scheme != Uri.UriSchemeHttp &&
                    Uri.Scheme != Uri.UriSchemeHttps)
                {
                    return $"{nameof(Uri)} scheme must be either HTTP or HTTPS.";
                }

                return null;
            }
        }

        public static implicit operator HttpAddress(Uri uri)
        {
            return new HttpAddress(uri);
        }
    }
}