using System;

namespace PatchKit.Network
{
    /// <summary>
    /// Extensions methods allowing for more declarative approach to byte range operations.
    /// </summary>
    public static class BytesRangeExtensions
    {
        /// <summary>
        /// Returns true if the Start and End of given range are equal.
        /// Otherwise returns false.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool IsEmpty(this BytesRange range)
        {
            return range.Start == range.End;
        }

        /// <summary>
        /// Returns true if given range is in form 0:-1.
        /// Otherwise returns false.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool IsFull(this BytesRange range)
        {
            return range.Start == 0 && range.End == -1;
        }

        /// <summary>
        /// Returns the size of a given range.
        /// Returns -1 if the range is not limited.
        /// </summary>
        public static long Size(this BytesRange range)
        {
            if (range.End == -1)
            {
                return -1;
            }

            return range.End - range.Start;
        }

        /// <summary>
        /// Limits one range inside the other, examples:
        /// 0:-1 contained in range 20:30 becomes 20:30.
        /// 0:100 contained in range 50:-1 becomes 50:100
        /// </summary>
        public static BytesRange ContainIn(this BytesRange range, BytesRange outer)
        {
            if (Contains(outer, range))
            {
                return range;
            }

            long start = range.Start;
            long end = range.End;

            if (range.Start < outer.Start)
            {
                start = outer.Start;
            }

            if (outer.End != -1)
            {
                if (range.End == -1 || range.End > outer.End)
                {
                    end = outer.End;
                }
            }

            return BytesRange.Make(start, end);
        }

        /// <summary>
        /// Localizes one range inside another, the resulting range is limited and local to the relative ("parent") range
        /// If the ranges don't overlap, an empty range will be returned.
        /// Examples:
        /// 50:95 localized within 10:90 becomes 40:-1
        /// 5:15 localized within 10:90 becomes 0:5
        /// </summary>
        public static BytesRange LocalizeTo(this BytesRange range, BytesRange relative)
        {
            if (!Overlaps(range, relative))
            {
                return BytesRange.Empty();
            }

            long localStart = range.Start >= relative.Start ? range.Start - relative.Start : 0;
            long localEnd = range.End <= relative.End ? range.End - relative.Start : -1;

            if (range.End == -1)
            {
                localEnd = -1;
            }

            return BytesRange.Make(localStart, localEnd);
        }

        /// <summary>
        /// Returns true if given ranges overlap.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool Overlaps(this BytesRange lhs, BytesRange rhs)
        {
            return Contains(lhs, rhs)
                || Contains(rhs, lhs)
                || Contains(lhs, rhs.Start)
                || Contains(lhs, rhs.End);
        }

        /// <summary>
        /// Returns true if the outer range fully contains the inner range.
        /// </summary>
        /// <param name="outer"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static bool Contains(this BytesRange outer, BytesRange inner)
        {
            if (inner.End == -1 && outer.End != -1)
            {
                return false;
            }

            if (inner.Start >= outer.Start)
            {
                return outer.End == -1 || inner.End <= outer.End;
            }

            return false;
        }
    
        /// <summary>
        /// Returns true if the given range contains the specified value.
        /// </summary>
        /// <param name="range"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(this BytesRange range, long value)
        {
            return value >= range.Start && (range.End == -1 || value <= range.End);
        }
    }
}