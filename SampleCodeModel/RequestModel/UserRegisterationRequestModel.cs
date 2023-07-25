using FluentValidation;
using Newtonsoft.Json;


namespace SampleCodeModel.RequestModel
{
    /// <summary>
    /// UserRegisterationRequestModel
    /// </summary>
    public class UserRegisterationRequestModel
    {
        /// <summary>
        /// The value that assign as Email Address
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// The value that assign as First Name
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The value that assign as First Name
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The value that assign as password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class UserRegisterationRequestModelValidator : AbstractValidator<UserRegisterationRequestModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserRegisterationRequestModelValidator()
        {
            this.RuleFor(x => x.Email).NotEmpty().NotNull().MinimumLength(1).MaximumLength(50);
            this.RuleFor(x => x.FirstName).NotEmpty().NotNull().MinimumLength(1).MaximumLength(50);
            this.RuleFor(x => x.LastName).NotEmpty().NotNull().MinimumLength(1).MaximumLength(50);
            this.RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(1).MaximumLength(30);
        }
    }
}
