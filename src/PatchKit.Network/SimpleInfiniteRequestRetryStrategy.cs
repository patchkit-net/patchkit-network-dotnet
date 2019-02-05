namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Simple implementation of <see cref="T:PatchKit.Network.IRequestRetryDelayCalculator" /> that never stops making retries.
    /// </summary>
    public class SimpleInfiniteRequestRetryStrategy : IRequestRetryStrategy
    {
        /// <inheritdoc />
        public int DelayBeforeNextTry => 5 * 1000;

        /// <inheritdoc />
        public bool ShouldRetry => true;

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