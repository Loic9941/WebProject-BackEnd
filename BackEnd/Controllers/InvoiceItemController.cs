using BLL.IService;
using Domain;
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
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "DeliveryPartner, Administrator, Artisan")]
        [HttpGet]
        public ActionResult<IEnumerable<InvoiceItem>> GetInvoiceItems()
        {
            try
            {
                IEnumerable<InvoiceItem> listInvoiceItems = _invoiceItemService.GetInvoiceItems();
                return Ok(listInvoiceItems);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin, DeliveryPertner, Artisan")]
        [HttpGet("{id}", Name = "GetInvoiceItem")]
        public ActionResult GetInvoiceItem(int id) 
        {
            try
            {
                InvoiceItem? invoiceItem = _invoiceItemService.GetById(id);
                if (invoiceItem == null)
                {
                    return NotFound();
                }
                return Ok(invoiceItem);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
