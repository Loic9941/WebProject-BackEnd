using BLL.DTOs.InputDTOs;
using BLL.DTOs.OutputDTOs;
using BLL.IService;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/invoices")]
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
        public ActionResult<InvoiceOutputDTO> AddToShoppingCart(int productId)
        {
            try
            {
                Invoice invoice = _invoiceService.AddToInvoice(productId);
                return Ok(invoice.MapToDTO());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet(Name = "GetInvoices")]
        public ActionResult<IEnumerable<InvoiceOutputDTO>> GetInvoice()
        {
            try
            {
                IEnumerable<Invoice> invoices = _invoiceService.Get();
                return Ok(invoices.Select(x => x.MapToDTO()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetPendingInvoice", Name = "GetPendingInvoice")]
        public ActionResult<InvoiceOutputDTO> GetPendingInvoice()
        {
            try
            {
                Invoice? invoice = _invoiceService.GetPendingInvoice();
                if (invoice is null)
                {
                    return Ok(null);
                }
                return Ok(invoice.MapToDTO());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}/MarkAsPaid", Name = "MarkAsPaid")]
        public ActionResult MarkAsPaid(int id, MarkInvoiceAsPaidDTO markAsPaidDTO)
        {
            try
            {
                _invoiceService.MarkAsPaid(id, markAsPaidDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}", Name = "GetInvoice")]
        public ActionResult<InvoiceOutputDTO> GetInvoice(int id)
        {
            try
            {
                Invoice? invoice = _invoiceService.GetById(id);
                if (invoice == null)
                {
                    throw new Exception("Invoice not found");
                }
                return Ok(invoice.MapToDTO());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
