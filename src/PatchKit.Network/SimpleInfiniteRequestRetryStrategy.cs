using System;

namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Simple implementation of <see cref="T:PatchKit.Network.IRequestRetryDelayCalculator" /> that never stops making retries.
    /// </summary>
    public class SimpleInfiniteRequestRetryStrategy : IRequestRetryStrategy
    {
        private readonly int[] _delaysInSeconds =
        {
            0, 1, 2, 4, 8, 16, 32
        };

        private int _index;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInfiniteRequestRetryStrategy"/> class.
        /// </summary>
        public SimpleInfiniteRequestRetryStrategy()
        {
            _index = 0;
        }

        /// <inheritdoc />
        public int DelayBeforeNextTry => _delaysInSeconds[_index] * 1000;

        /// <inheritdoc />
        public bool ShouldRetry => true;

        /// <inheritdoc />
        public void OnRequestSuccess()
        {
            _index = 0;
        }

        /// <inheritdoc />
        public void OnRequestFailure()
        {
            _index = Math.Min(_index + 1, _delaysInSeconds.Length - 1);
        }
    }
}