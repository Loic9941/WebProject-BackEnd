using BLL.DTOs.OutputDTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetUsers")]
        [Authorize(Roles= "Administrator")]
        public ActionResult<IEnumerable<UserOutputDTO>> GetUsers()
        {
            return Ok(_userService.GetUsers().Select(x => x.MapToDTO()));
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("DeliveryPartners", Name = "DeliveryPartners")]
        [Authorize(Roles = "Customer")]
        public ActionResult<IEnumerable<UserOutputDTO>> GetDeliveryPartners()
        {
            return Ok(_userService.GetDeliveryPartners().Select(x => x.MapToDTO()));
        }

        [HttpPut("{id}/Block", Name = "BlockUser")]
        [Authorize(Roles = "Administrator")]
        public ActionResult BlockUser(int id)
        {
            try
            {
                _userService.BlockUser(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/Unblock", Name = "UnblockUser")]
        [Authorize(Roles = "Administrator")]
        public ActionResult UnblockUser(int id)
        {
            try
            {
                _userService.UnblockUser(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
