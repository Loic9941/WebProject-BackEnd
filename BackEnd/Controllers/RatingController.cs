using BLL.DTOs;
using BLL.IService;
using BLL.Services;
using Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]

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

        [Authorize(Roles = "Artisan")]
        [HttpGet]
        [Route("ratings")]
        public ActionResult GetRatings()
        {
            try
            {
                var ratings = _ratingService.GetRatings();
                return Ok(ratings);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
