namespace PatchKit.Network
{
    /// <summary>
    /// Defines retry strategy for requests.
    /// Results are based on request success/failure events.
    /// </summary>
    public interface IRequestRetryStrategy
    {
        /// <summary>
        /// Delay (in miliseconds) that should be made before trying next request.
        /// </summary>
        int DelayBeforeNextTry { get; }

        /// <summary>
        /// States whether retry should be done.
        /// </summary>
        bool ShouldRetry { get; }
        
        /// <summary>
        /// Use it to notify about successful request.
        /// </summary>
        void OnRequestSuccess();
        
        /// <summary>
        /// Use it to notify about failed request.
        /// </summary>
        void OnRequestFailure();
    }
}