using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SampleCodeModel.Helper;
using SampleCodeModel.RequestModel;
using SampleCodeModel.ResponseModel;
using SampleCodeService.SampleCodeRepository.Interface;

namespace SampleCode.Controllers.V1
{
    /// <summary>
    /// AuthenticationController
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Declaration
        private IAuthenticationRepository _authenticationRepository;

        /// <summary>
        /// Initializing AuthenticationController
        /// </summary>
        /// <param name="authenticationRepository">Authentication Repository</param>
        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        #endregion

        #region Register User
        /// <summary>
        /// Register User 
        /// </summary>   
        /// <param name="model"></param>
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterFarmUser([CustomizeValidator(Interceptor = typeof(FluentInterceptor))] UserRegisterationRequestModel model)
        {
            await _authenticationRepository.RegisterUser(model);
            return StatusCode(StatusCodes.Status201Created);
        }

        #endregion

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Auth([FromBody] LoginRequestModel loginRequestModel)
        {
            var response = await _authenticationRepository
                    .Login(loginRequestModel);
            return Ok(response);
        }
        #endregion
    }
}
