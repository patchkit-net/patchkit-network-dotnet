namespace PatchKit.Network
{
    /// <summary>
    /// Type of HTTP POST request content.
    /// </summary>
    public enum HttpPostRequestContentType
    {
        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        // ReSharper disable once InconsistentNaming
        ApplicationXWWWFormUrlEncoded,

        /// <summary>
        /// multipart/form-data
        /// </summary>
        MultipartFormData
    }
}