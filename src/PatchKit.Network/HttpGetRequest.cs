using System;
using PatchKit.Core;

namespace PatchKit.Network
{
    /// <summary>
    /// Describes HTTP GET request.
    /// </summary>
    public struct HttpGetRequest : IValidatable
    {
        public HttpGetRequest(Uri address, BytesRange? range)
        {
            Address = address;
            Range = range;
        }

        /// <summary>
        /// Target request address.
        /// </summary>
        public Uri Address { get; }

        /// <summary>
        /// Requested bytes range.
        /// If set to <c>null</c> range is not specified.
        /// </summary>
        public BytesRange? Range { get; }

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

                if (!Range.IsValidOrNull())
                {
                    return Range.Value.GetFieldValidationError(nameof(Range));
                }

                return null;
            }
        }
    }
}