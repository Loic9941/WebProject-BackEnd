using Domain;

namespace BLL.IService
{
    public interface IInvoiceItemService
    {
        public void Delete(int invoiceItemId);

        public IEnumerable<InvoiceItem> GetInvoiceItems();
    }
}
