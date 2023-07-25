using Newtonsoft.Json;

namespace SampleCodeModel.CustomModels
{
    /// <summary>
    /// SearchRequestModel
    /// </summary>
    public class SearchRequestModel
    {
        /// <summary>
        /// Initialization SearchRequestModel
        /// </summary>
        public SearchRequestModel()
        {
            Page = 1;
            PageSize = 10;
        }

        /// <summary>
        /// Search string to look up for matching results. 
        /// </summary>
        [JsonProperty(PropertyName = "search_text")]
        public string? SearchText { get; set; }

        /// <summary>
        /// Expected page number in the result set.
        /// </summary>
        [JsonProperty(PropertyName = "page")]

        public int Page { get; set; }

        /// <summary>
        /// Page size of the result set.
        /// </summary>
        [JsonProperty(PropertyName = "page_size")]

        public int PageSize { get; set; }

    }
}
