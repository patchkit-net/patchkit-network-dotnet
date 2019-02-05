namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Simple implementation of <see cref="T:PatchKit.Network.IRequestTimeoutCalculator" />.
    /// Timeout is not reset after <see cref="OnRequestSuccess"/>.
    /// </summary>
    public class SimpleRequestTimeoutCalculator : IRequestTimeoutCalculator
    {
        /// <inheritdoc />
        public int Timeout => 10 * 1000;

        /// <inheritdoc />
        public void OnRequestSuccess()
        {
        }

        /// <inheritdoc />
        public void OnRequestFailure()
        {
        }
    }
}