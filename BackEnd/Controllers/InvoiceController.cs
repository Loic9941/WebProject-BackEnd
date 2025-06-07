using BLL.DTOs.InputDTOs;
using BLL.DTOs.OutputDTOs;
using BLL.IServices;
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

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet(Name = "GetInvoices")]
        public ActionResult<IEnumerable<InvoiceOutputDTO>> GetInvoices()
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
        [HttpGet("pending", Name = "pending")]
        public ActionResult<InvoiceOutputDTO> Pending()
        {
            try
            {
                Invoice? invoice = _invoiceService.Pending();
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
        [HttpPut("{id}/markAsPaid", Name = "markAsPaid")]
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
