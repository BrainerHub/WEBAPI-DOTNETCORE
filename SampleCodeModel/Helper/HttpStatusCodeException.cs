using Newtonsoft.Json.Linq;

namespace SampleCodeModel.Helper
{
    /// <summary>
    /// HttpStatusCodeException
    /// </summary>
    public class HttpStatusCodeException : Exception
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// ContentType
        /// </summary>
        public string? ContentType { get; set; } = @"text/plain";

        /// <summary>
        /// Code
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// HttpStatusCodeException Constructor
        /// </summary>
        /// <param name="status404NotFound"></param>
        /// <param name="statusCode"></param>
        public HttpStatusCodeException(object status404NotFound, int statusCode)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="Code"></param>
        public HttpStatusCodeException(int statusCode, string message, string Code = "0") : base(message)
        {
            this.ContentType = @"application/json";
            this.StatusCode = statusCode;
            this.Code = Code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="inner"></param>
        /// <param name="Code"></param>
        public HttpStatusCodeException(int statusCode, Exception inner, string Code = "0") : this(statusCode, inner.ToString(), Code) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="errorObject"></param>
        /// <param name="Code"></param>
        public HttpStatusCodeException(int statusCode, JObject errorObject, string Code = "0") : this(statusCode, errorObject.ToString(), Code)
        {
            this.ContentType = @"application/json";
        }
    }
}
