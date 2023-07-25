using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Net;
using SampleCodeModel.Helper;

namespace SampleCode.Helper
{

    /// <summary>
    /// HttpStatusCodeExtensions
    /// </summary>
    public static class HttpStatusCodeExtensions
    {
        /// <summary>
        /// ThrowException
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msgKey"></param>
        /// <param name="defaultMsg"></param>
        /// <returns></returns>
        public static Exception ThrowException(this HttpStatusCode code, string msgKey, string defaultMsg = "Server Exception while executing a request.")
        {
            var ex = new Exception(defaultMsg);
            ex.Data.Add("msg_key", msgKey);
            ex.Data.Add("status_code", code);
            return ex;
        }
        /// <summary>
        /// ThrowValidationMessage
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Exception ThrowValidationMessage(this string message)
        {
            var ex = new Exception(message);
            ex.Data.Add("status_code", HttpStatusCode.BadRequest);
            return ex;
        }

        /// <summary>
        /// ErrorResponse
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ErrorResult ErrorResponse(this HttpStatusCode code, string msg)
        {
            var result = new ErrorResult
            {
                Code = ((int)code),
                Message = code >= HttpStatusCode.Continue && code < HttpStatusCode.BadRequest ? null : msg
            };
            return result;
        }
        /// <summary>
        /// ErrorResponse
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static ErrorResult ErrorResponse(this HttpStatusCode code, Exception ex)
        {
            var result = new ErrorResult
            {
                Code = ((int)code),
                Message = code >= HttpStatusCode.Continue && code < HttpStatusCode.BadRequest ? null : ex.Message
            };
            return result;
        }

        /// <summary>
        /// AddOrReplaceQueryParameter
        /// </summary>
        /// <param name="c"></param>
        /// <param name="nameValues"></param>
        /// <returns></returns>
        public static string AddOrReplaceQueryParameter(this HttpContext c, params string[] nameValues)
        {
            if (nameValues.Length % 2 != 0)
            {
                throw new Exception("nameValues: has more parameters then values or more values then parameters");
            }
            var qps = new Dictionary<string, StringValues>();
            for (int i = 0; i < nameValues.Length; i += 2)
            {
                qps.Add(nameValues[i], nameValues[i + 1]);
            }
            return c.AddOrReplaceQueryParameters(qps);
        }

        /// <summary>
        /// AddOrReplaceQueryParameters
        /// </summary>
        /// <param name="c"></param>
        /// <param name="pvs"></param>
        /// <returns></returns>
        public static string AddOrReplaceQueryParameters(this HttpContext c, Dictionary<string, StringValues> pvs)
        {
            var request = c.Request;
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                //Port = request.Host.Port ?? 0,
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };

            var queryParams = QueryHelpers.ParseQuery(uriBuilder.Query);

            foreach (var (p, v) in pvs)
            {
                queryParams.Remove(p);
                queryParams.Add(p, v);
            }

            uriBuilder.Query = "";
            var allQPs = queryParams.ToDictionary(k => k.Key, k => k.Value.ToString());
            var url = QueryHelpers.AddQueryString(uriBuilder.ToString(), allQPs);

            return url;
        }
    }
}
