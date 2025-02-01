using System.IdentityModel.Tokens.Jwt;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PL.Identity;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticateController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try { 
                var token = await _authenticationService.Login(model.MapToDto());
                if (token == null)
                {
                    return Unauthorized();
                }
                return Ok(new
                {
                    token = token.token,
                    expiration = token.expiration
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("register-customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterModel model)
        {
            try
            {
                var result = await _authenticationService.RegisterCustomer(model.MapToDto());
                if (result == true)
                {
                    return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //fix me : we have to differentiate between user creation failed and user already exists
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            try
            {
                var result = await _authenticationService.RegisterAdmin(model.MapToDto());
                if (result == true)
                {
                    return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //fix me : we have to differentiate between user creation failed and user already exists
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
        }

        [HttpPost]
        [Route("register-artisan")]
        public async Task<IActionResult> RegisterArtisan([FromBody] RegisterModel model)
        {
            try
            {
                var result = await _authenticationService.RegisterArtisan(model.MapToDto());
                if (result == true)
                {
                    return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //fix me : we have to differentiate between user creation failed and user already exists
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            }
        }

        [HttpPost]
        [Route("register-delivery-partner")]
        public async Task<IActionResult> RegisterDeliveryPartner([FromBody] RegisterModel model)
        {
            try
            {
                var result = await _authenticationService.RegisterArtisan(model.MapToDto());
                if (result == true)
                {
                    return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //fix me : we have to differentiate between user creation failed and user already exists
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            }
        }
    }
}
