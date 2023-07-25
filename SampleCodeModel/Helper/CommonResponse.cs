using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeModel.Helper
{
    /// <summary>
    /// NotFoundResponse
    /// </summary>
    public class NotFoundResponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }

        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string? message { get; set; }

        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }

    }

    /// <summary>
    /// BadRequestResponse
    /// </summary>
    public class BadRequestResponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }
        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string? message { get; set; }

        /// <summary>
        ///  The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }
    }

    /// <summary>
    /// InternalServerErrorReponse
    /// </summary>
    public class InternalServerErrorReponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }
        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string? message { get; set; }

        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }
    }

    /// <summary>
    /// UnauthorizedResponse
    /// </summary>
    public class UnauthorizedResponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }

        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string? message { get; set; }

        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }

    }
}
