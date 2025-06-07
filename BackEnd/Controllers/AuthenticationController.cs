using BLL.DTOs.InputDTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        protected readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult Register(RegisterDTO registerDTO)
        {
            try
            {
                _authenticationService.RegisterUser(registerDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                var token = _authenticationService.Login(loginDTO);
                return Ok(new { Token = token });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
