using Microsoft.AspNetCore.Mvc;
using server.Dtos.User;
using server.Services.Auth;
using server.Services.Token;
using System.Net;

namespace server.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenService tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult<RegisterUserDto>> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                var response = await _authService.Register(registerUserDto);
                return Json(CreatedAtAction(nameof(Register), response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserCredentials)
        {
            try
            {
                //var response = await _authService.Login(loginUserCredentials);
                //return Json(CreatedAtAction(nameof(Register), response));
                var userToken = await _authService.Login(loginUserCredentials); // we not storing anymore token in response, we set to cookig

                HttpContext.Response.Cookies.Append("token", userToken.Token,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        HttpOnly = true,
                        Secure = true,
                        IsEssential = true,
                        SameSite = SameSiteMode.None,
                    });

                return Json(Ok(userToken)); // we dont need to send a jwt, cookie will be in the header of the response
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }
    }
}
