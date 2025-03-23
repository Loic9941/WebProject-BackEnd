using Domain;
using Microsoft.Identity.Client;

namespace BLL.IService
{
    public interface IInvoiceService
    {
        public Invoice AddToInvoice(int Id);

        public Invoice? GetPendingInvoice();

        public Invoice? GetById(int Id);

        public IEnumerable<Invoice> Get();

    }
}
