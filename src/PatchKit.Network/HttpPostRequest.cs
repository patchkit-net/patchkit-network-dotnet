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
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPostRequest"/> struct.
        /// </summary>
        public HttpPostRequest(HttpAddress address,
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
        public HttpAddress Address { get; }

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
                if (!Address.IsValid())
                {
                    return Address.GetFieldValidationError(nameof(Address));
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