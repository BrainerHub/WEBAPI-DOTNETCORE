
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleCodeModel.Helper;

namespace SampleCode
{
    /// <summary>
    /// FluentInterceptor
    /// </summary>
    public class FluentInterceptor : IValidatorInterceptor
    {

        /// <summary>
        /// FluentInterceptor Constructor
        /// </summary>
        public FluentInterceptor()
        {

        }

        /// <summary>
        /// AfterAspNetValidation
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="validationContext"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        FluentValidation.Results.ValidationResult IValidatorInterceptor.AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, FluentValidation.Results.ValidationResult result)
        {
            if (!result.IsValid)
            {
                FluentErrorResponse errorResponse = new FluentErrorResponse();
                errorResponse.Code = 400;
                errorResponse.Status = StatusCodes.Status400BadRequest;
                errorResponse.Message = "Request model is not valid";

                JObject jObject = new JObject();
                foreach (var item in result.Errors)
                {
                    if (!jObject.ContainsKey(item.PropertyName))
                        jObject.Add(item.PropertyName, item.ErrorMessage);
                }
                errorResponse.Errors = jObject;
                errorResponse.isFluentError = true;
                throw new Exception(JsonConvert.SerializeObject(errorResponse));
            }
            return result;
        }


        /// <summary>
        /// BeforeMvcValidation
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="commonContext"></param>
        /// <returns></returns>
        IValidationContext IValidatorInterceptor.BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}
