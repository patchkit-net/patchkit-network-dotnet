using PatchKit.Core;

namespace PatchKit.Network
{
    /// <summary>
    /// Specifies byte range.
    /// </summary>
    public struct BytesRange : IValidatable
    {
        /// <summary>
        /// Starting byte of the range (inclusive).
        /// </summary>
        public long Start { get; }

        /// <summary>
        /// Ending byte of the range (inclusive).
        /// If set to null, range is extended to the end.
        /// </summary>
        public long? End { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="BytesRange"/> struct.
        /// </summary>
        public BytesRange(long start, long? end)
        {
            Start = start;
            End = end;
        }

        /// <inheritdoc />
        public string ValidationError
        {
            get
            {
                if (Start < 0)
                {
                    return "Start cannot be less than zero.";
                }

                if (End.HasValue)
                {
                    if (End.Value < 0)
                    {
                        return "End cannot be less than zero.";
                    }

                    if (End.Value < Start)
                    {
                        return "End cannot be less than Start.";
                    }
                }

                return null;
            }
        }
    }
}