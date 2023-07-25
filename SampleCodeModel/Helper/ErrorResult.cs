using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeModel.Helper
{
    /// <summary>
    ///  ErrorResult
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; }

        /// <summary>
        /// MoreInfo
        /// </summary>
        [JsonProperty(PropertyName = "more_info")]
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
    }
}
