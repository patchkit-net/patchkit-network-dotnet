using System;

namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Simple implementation of <see cref="T:PatchKit.Network.IRequestTimeoutCalculator" />.
    /// Timeout is not reset after <see cref="OnRequestSuccess"/>.
    /// </summary>
    public class SimpleRequestTimeoutCalculator : IRequestTimeoutCalculator
    {
        private readonly int[] _timeoutsInSeconds =
        {
            10, 15, 30, 60, 120
        };

        private int _index;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleRequestTimeoutCalculator"/> class.
        /// </summary>
        public SimpleRequestTimeoutCalculator()
        {
            _index = 0;
        }

        /// <inheritdoc />
        public int Timeout => _timeoutsInSeconds[_index] * 1000;

        /// <inheritdoc />
        public void OnRequestSuccess()
        {
        }

        /// <inheritdoc />
        public void OnRequestFailure()
        {
            _index = Math.Min(_index + 1, _timeoutsInSeconds.Length - 1);
        }
    }
}