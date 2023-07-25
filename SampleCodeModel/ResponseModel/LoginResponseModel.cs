using Newtonsoft.Json;
using System.Collections.Generic;

namespace SampleCodeModel.ResponseModel
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginResponseModel
    {

        /// <summary>
        /// The value that assign as Email Address
        /// </summary>
        [JsonProperty(PropertyName = "email_address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The value that assign as First Name
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The value that assign as Last Name
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The string that assign as AccessToken
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// The string that assign as Expires in
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int Expires_in { get; set; }

    }
}
