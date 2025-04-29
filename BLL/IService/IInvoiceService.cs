using BLL.DTOs.InputDTOs;
using Domain;

namespace BLL.IService
{
    public interface IInvoiceService
    {
        public Invoice? GetPendingInvoice();

        public Invoice? GetById(int id);

        public IEnumerable<Invoice> Get();

        public void MarkAsPaid(int invoiceId, MarkInvoiceAsPaidDTO markAsPaidDTO);

    }
}
