using BLL.IService;
using DAL.Repository;
using Domain;

namespace BLL.Services
{
    public class InvoiceService : IInvoiceService
    {

        protected readonly IAuthenticationService _authenticationService;
        protected readonly IGenericRepository<Invoice> _invoiceRepository;
        protected readonly IGenericRepository<InvoiceItem> _invoiceItemRepository;
        protected readonly IGenericRepository<Product> _productRepository;

        public InvoiceService(
            IAuthenticationService authenticationService,
            IGenericRepository<Invoice> invoiceRepository,
            IGenericRepository<InvoiceItem> invoiceItemRepository,
            IGenericRepository<Product> productRepository
        )
        {
            _authenticationService = authenticationService;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _productRepository = productRepository;
        }

        public Invoice? GetById(int Id)
        {  
            return _invoiceRepository.GetSingleOrDefault(x => x.Id == Id, "InvoiceItems,InvoiceItems.Product");
        }

        public IEnumerable<Invoice> Get()
        {
            return _invoiceRepository.Get();
        }

        public Invoice? GetPendingInvoice()
        {
            var userId = _authenticationService.GetUserId() ?? throw new Exception("User not found");
            return _invoiceRepository.GetSingleOrDefault(x => x.UserId == userId && x.Status == "pending", "InvoiceItems");
        }

        public Invoice AddToInvoice(int Id)
        {
            int invoiceId = 0;

            Product? product = this._productRepository.GetSingleOrDefault(x => x.Id == Id) ?? throw new Exception("Product not found");
            var userId = _authenticationService.GetUserId() ?? throw new Exception("User not found");
            Invoice? invoice = _invoiceRepository.GetSingleOrDefault(x => x.Status == "Pending" && x.UserId == userId);
            if (invoice == null)
            {
                invoice = new Invoice { UserId = userId, Status = "pending" };
                invoiceId = _invoiceRepository.Add(invoice);
            }
            else
            {
                invoiceId = invoice.Id;
            }
            //check if there is already an invoice item with the same product
            var existingInvoiceItem = _invoiceItemRepository.GetSingleOrDefault(x => x.InvoiceId == invoiceId && x.ProductId == Id);
            if (existingInvoiceItem == null) {
                _invoiceItemRepository.Add(new InvoiceItem
                {
                    InvoiceId = invoiceId,
                    ProductId = Id,
                    UnitPrice = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                existingInvoiceItem.UnitPrice = product.Price; // to be sure to have the last price
                existingInvoiceItem.Quantity += 1;
                _invoiceItemRepository.Update(existingInvoiceItem);
            }
            return this.GetById(invoiceId) ?? throw new Exception("Invoice not found");
        }
    }
}
