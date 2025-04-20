using BLL.DTOs.OutputDTOs;
using BLL.IService;
using BLL.Services;
using Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ratings")]

    public class RatingController : ControllerBase
    {
        protected readonly IRatingService _ratingService;
        public RatingController(
            IRatingService ratingService,
            IInvoiceItemService invoiceItemService
            )
        {
            _ratingService = ratingService;
        }

        [Authorize(Roles = "Artisan,Customer")]
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<RatingOutputDTO>> GetRatings([FromQuery] int? productId)
        {
            try
            {
                return Ok(_ratingService.GetRatings(productId).Select(x => x.MapToDTO()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
