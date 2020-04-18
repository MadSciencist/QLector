using Newtonsoft.Json;

namespace QLector.Application.Core
{
    /// <summary>
    /// Basic application response indicating whether operation succeeded
    /// </summary>
    public class IsSuccessResponse
    {
        public IsSuccessResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Indicating if operation succeeded
        /// </summary>
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; }
    }
}
