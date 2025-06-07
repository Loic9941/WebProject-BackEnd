using BLL.DTOs.InputDTOs;
using Domain;

namespace BLL.IServices
{
    public interface IInvoiceService
    {
        public Invoice? Pending();

        public Invoice? GetById(int id);

        public IEnumerable<Invoice> Get();

        public void MarkAsPaid(int invoiceId, MarkInvoiceAsPaidDTO markAsPaidDTO);

    }
}
