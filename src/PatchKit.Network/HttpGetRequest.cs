using PatchKit.Core;

namespace PatchKit.Network
{
    /// <summary>
    /// Describes HTTP GET request.
    /// </summary>
    public struct HttpGetRequest : IValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpGetRequest"/> struct.
        /// </summary>
        public HttpGetRequest(HttpAddress address)
        {
            Address = address;
        }

        /// <summary>
        /// Target request address.
        /// </summary>
        public HttpAddress Address { get; }

        /// <inheritdoc />
        public string ValidationError
        {
            get
            {
                if (!Address.IsValid())
                {
                    return Address.GetFieldValidationError(nameof(Address));
                }

                return null;
            }
        }
    }
}