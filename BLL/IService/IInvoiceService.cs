using BLL.DTOs;
using Domain;
using Microsoft.Identity.Client;

namespace BLL.IService
{
    public interface IInvoiceService
    {
        public Invoice AddToInvoice(int id);

        public Invoice? GetPendingInvoice();

        public Invoice? GetById(int id);

        public IEnumerable<Invoice> Get();

        public void MarkAsPaid(int invoiceId, MarkInvoiceAsPaidDTO markAsPaidDTO);

    }
}
