using FluentValidation;
using Newtonsoft.Json;

namespace SampleCodeModel.RequestModel
{ 
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
    {

        /// <summary>
        /// 
        /// </summary>
        public LoginRequestModelValidator()
        {

            RuleFor(model => model.EmailAddress).NotNull().NotEmpty();
            RuleFor(model => model.Password).NotNull().NotEmpty();

        }
    }


    /// <summary>
    /// LoginRequestModel
    /// </summary>
    /// 
    public class LoginRequestModel
    {
        /// <summary>
        /// The value that assign as Email Address
        /// </summary>
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The value that assign as password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
