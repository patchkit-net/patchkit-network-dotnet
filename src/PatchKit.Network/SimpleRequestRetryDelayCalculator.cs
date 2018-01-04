using System;

namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Simple implementation of <see cref="T:PatchKit.Network.IRequestRetryDelayCalculator" />.
    /// </summary>
    public class SimpleRequestRetryDelayCalculator : IRequestRetryDelayCalculator
    {
        private readonly int[] _delaysInSeconds =
        {
            0, 1, 2, 4, 8, 16, 32
        };

        private int _index;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleRequestRetryDelayCalculator"/> class.
        /// </summary>
        public SimpleRequestRetryDelayCalculator()
        {
            _index = 0;
        }

        /// <inheritdoc />
        public int Delay => _delaysInSeconds[_index] * 1000;

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