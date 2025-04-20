using BLL.DTOs.InputDTOs;
using BLL.DTOs.OutputDTOs;
using BLL.IService;
using Domain;
using Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceItemController : ControllerBase
    {
        protected IInvoiceItemService _invoiceItemService;

        public InvoiceItemController(IInvoiceItemService invoiceItemService)
        {
            _invoiceItemService = invoiceItemService;
        }

        [Authorize(Roles = "Customer, Administrator")]
        [HttpDelete("{id}", Name = "DeleteInvoiceItem")]
        public ActionResult DeleteInvoiceItem(int id)
        {
            try
            {
                _invoiceItemService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "DeliveryPartner, Administrator, Artisan")]
        [HttpGet]
        public ActionResult<IEnumerable<InvoiceItemOutputDTO>> GetInvoiceItems()
        {
            try
            {
                IEnumerable<InvoiceItem> listInvoiceItems = _invoiceItemService.GetInvoiceItems();
                return Ok(listInvoiceItems.Select(x => x.MapToDTO()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetInvoiceItem")]
        public ActionResult<InvoiceItemOutputDTO> GetInvoiceItem(int id) 
        {
            try
            {
                InvoiceItem? invoiceItem = _invoiceItemService.GetById(id);
                if (invoiceItem == null)
                {
                    return NotFound();
                }
                return Ok(invoiceItem.MapToDTO());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin, Artisan")]
        [HttpPut("{id}/markAsReadyToBeShipped", Name = "MarkAsReadyToBeShipped")]
        public ActionResult<InvoiceItemOutputDTO> MarkAsReadyToBeShipped(int id)
        {
            try
            {
                var invoiceItem = _invoiceItemService.MarkAsReadyToBeShipped(id);
                return Ok(invoiceItem.MapToDTO());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin, DeliveryPartner")]
        [HttpPut("{id}/markAsPickedUp", Name = "MarkAsPickedUp")]
        public ActionResult<InvoiceItemOutputDTO> MarkAsPickedUp(int id, MarkInvoiceItemAsDTO markInvoiceItemAsDTO)
        {
            try
            {
                var invoiceItem = _invoiceItemService.MarkAsPickedUp(id, markInvoiceItemAsDTO);
                return Ok(invoiceItem.MapToDTO());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin, DeliveryPartner")]
        [HttpPut("{id}/markAsInTransit", Name = "MarkAsInTransit")]
        public ActionResult<InvoiceItemOutputDTO> MarkAsInTransit(int id, MarkInvoiceItemAsDTO markInvoiceItemAsDTO)
        {
            try
            {

                var invoiceItem = _invoiceItemService.MarkAsInTransit(id, markInvoiceItemAsDTO);
                return Ok(invoiceItem.MapToDTO());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin, DeliveryPartner")]
        [HttpPut("{id}/markAsDelivered", Name = "markAsDelivered")]
        public ActionResult<InvoiceItemOutputDTO> MarkAsDelivered(int id)
        {
            try
            {
                var invoiceItem = _invoiceItemService.MarkAsDelivered(id);
                return Ok(invoiceItem.MapToDTO());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Customer,Administrator")]
        [HttpPost("{id}/rate/", Name = "RateInvoiceItem")]
        public ActionResult RateInvoiceItem(int id, RateProductDTO rateProductDTO)
        {
            try
            {
                _invoiceItemService.Rate(id, rateProductDTO);
                return NoContent();
            }
            catch (RatingConflict e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
