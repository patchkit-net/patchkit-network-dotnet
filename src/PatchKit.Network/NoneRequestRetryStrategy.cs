namespace PatchKit.Network
{
    /// <inheritdoc />
    /// <summary>
    /// Strategy that never allows to make a retry.
    /// </summary>
    public class NoneRequestRetryStrategy : IRequestRetryStrategy
    {
        /// <inheritdoc />
        public int DelayBeforeNextTry => 0;

        /// <inheritdoc />
        public bool ShouldRetry => false;

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