using BLL.DTOs;
using BLL.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        protected readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
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

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult Login(LoginDTO loginDTO)
        {

            var token = _authenticationService.Login(loginDTO);
            return Ok(new { Token = token });
        }
    }
}
