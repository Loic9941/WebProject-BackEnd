using BLL.DTOs.InputDTOs;
using BLL.DTOs.OutputDTOs;
using BLL.IServices;
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

        [Authorize(Roles = "Artisan")]
        [HttpGet]
        [Route("{id}")]
        public ActionResult<RatingOutputDTO> GetRating(int id)
        {
            try
            {
                RatingOutputDTO? rating = _ratingService.GetRating(id)?.MapToDTO();
                if (rating == null)
                {
                    return NotFound();
                }
                return Ok(rating);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Artisan")]
        [HttpPost]
        [Route("{id}/comments")]
        public ActionResult<RatingOutputDTO> AddComment(int id, CommentDTO commentDTO)
        {
            try
            {
                _ratingService.SaveComment(id, null, commentDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Artisan")]
        [HttpPut]
        [Route("{id}/comments/{commentId}")]
        public ActionResult<RatingOutputDTO> UpdateComment(int id, int commentId, CommentDTO commentDTO)
        {
            try
            {
                _ratingService.SaveComment(id, commentId, commentDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
