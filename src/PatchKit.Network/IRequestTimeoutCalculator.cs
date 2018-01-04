namespace PatchKit.Network
{
    /// <summary>
    /// Calculates request timeout based on success/failure events.
    /// </summary>
    public interface IRequestTimeoutCalculator
    {
        /// <summary>
        /// Timeout in miliseconds.
        /// </summary>
        int Timeout { get; }

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