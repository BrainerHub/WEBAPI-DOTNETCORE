using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeModel.RequestModel
{

    /// <summary>
    /// ProductRequestModel
    /// </summary>
    public class ProductRequestModel
    {
        /// <summary>
        /// The value that assign as Name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The value that assign as Description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// The value that assign as Price
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// The value that assign as Quantity
        /// </summary>
        public long? quantity { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProductUpdateRequestModel : ProductRequestModel
    {
        public List<int> DeleteImageId { get; set; }
    }
}
