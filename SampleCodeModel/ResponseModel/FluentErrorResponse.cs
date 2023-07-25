using Newtonsoft.Json.Linq;

namespace SampleCodeModel.ResponseModel
{
    /// <summary>
    /// FluentErrorResponse
    /// </summary>
    public class FluentErrorResponse
    {
        /// <summary>
        /// Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// isFluentError
        /// </summary>
        public bool isFluentError { get; set; }

        /// <summary>
        /// Errors
        /// </summary>
        public JObject? Errors { get; set; }
    }
}
