using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeModel.Helper
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
