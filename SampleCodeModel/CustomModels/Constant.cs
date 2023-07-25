using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodeModel.CustomModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// 
        /// </summary>
        public static class JWTTokenCredentials
        {
            public static string ValidIssuer = "https://localhost:44338";
            public static string ValidAudience = "https://localhost:44338";
            public static string IssuerSigningKeyBytes = "c26faf6b-d639-4673-b761-ba03e6384ea";
            public static SecurityKey GetSecurityKey() =>
               new SymmetricSecurityKey(Encoding.ASCII.GetBytes(IssuerSigningKeyBytes));
        }

        /// <summary>
        /// 
        /// </summary>
        public class JWTClaimParameters
        {
            public const string EmailAddress = "EmailAddress";
            public const string UserId = "UserId";
        }
    }
}
