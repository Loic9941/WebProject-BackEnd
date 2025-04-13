using BLL.DTOs;
using Domain;

namespace BLL.IService
{
    public interface IInvoiceItemService
    {
        public void Delete(int invoiceItemId);

        public IEnumerable<InvoiceItem> GetInvoiceItems();

        public InvoiceItem? GetById(int id);

        public InvoiceItem? MarkAsReadyToBeShipped(int id);
        public InvoiceItem? MarkAsPickedUp(int id, MarkInvoiceItemAsDTO markInvoiceItemAsDTO);
        public InvoiceItem? MarkAsInTransit(int id, MarkInvoiceItemAsDTO markInvoiceItemAsDTO);
        public InvoiceItem? MarkAsDelivered(int id);
    }
}
