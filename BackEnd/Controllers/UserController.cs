using BLL.IService;
using BLL.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet(Name = "GetUsers")]
        [Authorize(Roles= "Administrator")]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
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

        [HttpGet("GetDeliveryPartners", Name = "GetDeliveryPartners")]
        [Authorize(Roles = "Customer")]
        public ActionResult<IEnumerable<User>> GetDeliveryPartners()
        {
            return Ok(_userService.GetDeliveryPartners());
        }
    }
}
