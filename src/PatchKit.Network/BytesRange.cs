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
        public BytesRange(long start, long end)
        {
            Start = start;
            End = end;
        }
        
        /// <summary>
        /// Returns a range specified by the start and end arguments.
        /// If an the optional topBound argument is provided, the end of the range
        /// will be limited by it.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="topBound"></param>
        /// <returns></returns>
        public static BytesRange Make(long start, long end = -1, long topBound = -1)
        {
            long s = start;
            long e = end;

            if (end == -1 && topBound != -1)
            {
                end = topBound;
            }

            return new BytesRange
            {
                Start = s,
                End = e
            };
        }

        /// <summary>
        /// Returns an empty range 0:0
        /// </summary>
        /// <returns></returns>
        public static BytesRange Empty()
        {
            return Make(0, 0);
        }
    }
}