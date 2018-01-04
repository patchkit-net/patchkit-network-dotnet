namespace PatchKit.Network
{
    /// <summary>
    /// Calculates delay before next request based on success/failure events.
    /// </summary>
    public interface IRequestRetryDelayCalculator
    {
        /// <summary>
        /// Delay in miliseconds.
        /// </summary>
        int Delay { get; }

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