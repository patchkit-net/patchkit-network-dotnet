using System;
using PatchKit.Core;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Network
{
    /// <summary>
    /// Describes HTTP POST request.
    /// </summary>
    public struct HttpPostRequest : IValidatable
    {
        public HttpPostRequest(Uri address,
            ImmutableArray<byte> content,
            HttpPostRequestContentType contentType)
        {
            Address = address;
            Content = content;
            ContentType = contentType;
        }

        /// <summary>
        /// Target request address.
        /// </summary>
        public Uri Address { get; }

        /// <summary>
        /// Content type.
        /// </summary>
        public HttpPostRequestContentType ContentType { get; }

        /// <summary>
        /// Content data.
        /// </summary>
        public ImmutableArray<byte> Content { get; }

        /// <inheritdoc />
        public string ValidationError
        {
            get
            {
                if (Address == null)
                {
                    return "Address cannot be null.";
                }

                if (Address.Scheme != Uri.UriSchemeHttp &&
                    Address.Scheme != Uri.UriSchemeHttps)
                {
                    return "Address scheme must be HTTP or HTTPS";
                }

                if (!Enum.IsDefined(typeof(HttpPostRequestContentType), ContentType))
                {
                    return "ContentType must be defined enum value.";
                }

                return null;
            }
        }
    }
}