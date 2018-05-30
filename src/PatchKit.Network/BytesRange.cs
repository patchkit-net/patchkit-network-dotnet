namespace PatchKit.Network
{
    /// <summary>
    /// Specifies byte range.
    /// </summary>
    public struct BytesRange
    {
        /// <summary>
        /// Starting byte of range (inclusive).
        /// </summary>
        public long Start;

        /// <summary>
        /// Ending byte of range (inclusive).
        /// If set to negative value, range is extended to the maximum possible value.
        /// </summary>
        public long End;

        /// <summary>
        /// Initializes a new instance of <see cref="BytesRange"/> struct.
        /// </summary>
        public BytesRange(long start, long end = -1)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Returns an empty range 0:0
        /// </summary>
        /// <returns></returns>
        public static BytesRange Empty()
        {
            return new BytesRange(0, 0);
        }
    }
}