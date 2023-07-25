
using Microsoft.EntityFrameworkCore;
using SampleCode.Helper;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SampleCodeService.SampleCodeRepository.Interface;
using SampleCodeService.SampleCodeRepository.Implementation;
using SampleCodeModel.RequestModel;
using static SampleCodeModel.CustomModels.Constant;
using SampleCodeModel.Helper;

namespace SampleCode
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                   .AddNewtonsoftJson(options =>
                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                       );
            var _configuration = builder.Configuration;

            #region Set DB Connection
            string? connectionstring = _configuration.GetConnectionString("DefaultConnection");


            builder.Services.AddDbContext<SampleCodeModel.DbEntities.SampleContex>((serviceProvider, options) =>
            {

                options.UseSqlServer(connectionstring,
                                               sqlServerOptionsAction: sqlOptions =>
                                               {
                                                   sqlOptions.EnableRetryOnFailure();
                                               });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(true);
            });
            #endregion

            #region DependencyInjection
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            #endregion

            #region Add FluentValidation
#pragma warning disable CS0618 // Type or member is obsolete
            builder.Services
        .AddMvc()
        // Adds fluent validators to Asp.net
        .AddFluentValidation(c =>
        {
            c.RegisterValidatorsFromAssemblyContaining<UserRegisterationRequestModel>();
            // Optionally set validator factory if you have problems with scope resolve inside validators.
            c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
        });
#pragma warning restore CS0618 // Type or member is obsolete

            builder.Services.AddTransient<IValidatorInterceptor, FluentInterceptor>();

            builder.Services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JWTTokenCredentials.ValidIssuer,
                    ValidAudience = JWTTokenCredentials.ValidAudience,
                    IssuerSigningKey = JWTTokenCredentials.GetSecurityKey() // new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JWTTokenCredentials.IssuerSigningKeyBytes))
                };
                jwtOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (!string.IsNullOrWhiteSpace(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var jwtToken = context.SecurityToken as JwtSecurityToken;
                        context.Request.Headers[ClaimTypes.Email] = jwtToken.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;

                        var GivenName = jwtToken.Claims.Where(x => x.Type == ClaimTypes.GivenName).FirstOrDefault()?.Value;
                        if (string.IsNullOrWhiteSpace(GivenName))
                        {

                            context.Request.Headers[JWTClaimParameters.UserId] = jwtToken.Claims.Where(x => x.Type == JWTClaimParameters.UserId).FirstOrDefault().Value;
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        var result = new ErrorResult
                        {
                            Code = 401,
                            Status = StatusCodes.Status401Unauthorized,
                            Message = "Unauthorized"
                        };
                        JsonSerializerSettings settings = new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            NullValueHandling = NullValueHandling.Ignore
                        };
                        var content = JsonConvert.SerializeObject(result, settings);
                        context.Response.ContentType = "application/json";
                        context.Response.WriteAsync(content);
                        context.HandleResponse();

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();


            builder.Services.AddFluentValidationRulesToSwagger();

            builder.Services.AddSwaggerGen();

            #endregion

            #region Add Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGenNewtonsoftSupport();
            #endregion

            builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization();

            var app = builder.Build();

            app.UseHttpStatusCodeExceptionMiddleware();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}