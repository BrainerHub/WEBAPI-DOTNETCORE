using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeModel.ResponseModel
{
    /// <summary>
    /// ProductResponseModel
    /// </summary>
    public class ProductResponseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("productid")]
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("description")]
        public String Description { get; set; }

        /// <summary>
        /// The value that assign as Price
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// The value that assign as Quantity
        /// </summary>
        public long? quantity { get; set; }
    }
}
