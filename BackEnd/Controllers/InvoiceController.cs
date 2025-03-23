using BLL.IService;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        protected readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("AddToInvoice/{productId}", Name = "AddToShoppingCart")]
        public ActionResult<Invoice> AddToShoppingCart(int productId)
        {
            try
            {
                Invoice invoice = _invoiceService.AddToInvoice(productId);
                return Ok(invoice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(Name = "GetInvoices")]
        public ActionResult<Invoice> GetInvoice()
        {
            try
            {
                IEnumerable<Invoice> invoices = _invoiceService.Get();
                return Ok(invoices);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetPendingInvoice", Name = "GetPendingInvoice")]
        public ActionResult<Invoice> GetPendingInvoice()
        {
            try
            {
                Invoice? invoice = _invoiceService.GetPendingInvoice();
                return Ok(invoice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}", Name = "GetInvoice")]
        public ActionResult<Invoice> GetInvoice(int id)
        {
            try
            {
                Invoice? invoice = _invoiceService.GetById(id);
                if (invoice == null)
                {
                    throw new Exception("Invoice not found");
                }
                return Ok(invoice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
