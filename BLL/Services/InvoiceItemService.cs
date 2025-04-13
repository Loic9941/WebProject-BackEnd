using BLL.IService;
using DAL.Repository;
using Domain;

namespace BLL.Services
{
    public class InvoiceItemService : IInvoiceItemService
    {
        protected IGenericRepository<InvoiceItem> _invoiceItemRepository;
        protected IAuthenticationService _authenticationService;

        public InvoiceItemService(
            IGenericRepository<InvoiceItem> invoiceItemRepository,
            IAuthenticationService authenticationService
            )
        {
            _invoiceItemRepository = invoiceItemRepository;
            _authenticationService = authenticationService;
        }

        public void Delete(int invoiceItemId)
        {
            InvoiceItem invoiceItem = _invoiceItemRepository.GetSingleOrDefault(InvoiceItem => InvoiceItem.Id == invoiceItemId, "Invoice") ?? throw new Exception("InvoiceItem not found");
            var userId = _authenticationService.GetUserId() ?? throw new Exception("User not found");
            if (_authenticationService.GetUserId() == invoiceItem.Invoice.UserId || _authenticationService.IsAdmin())
            {
                _invoiceItemRepository.Delete(invoiceItem);
            }
            else
            {
                throw new Exception("You are not authorized to delete this invoice item");
            }
        }

        public InvoiceItem? GetById(int id)
        {
            return _invoiceItemRepository.GetSingleOrDefault(
                x => x.Id == id,
                "Product");
        }

        public IEnumerable<InvoiceItem> GetInvoiceItems()
        {
            var userId = _authenticationService.GetUserId() ?? throw new Exception("User not found");
            if (_authenticationService.IsArtisan())
            {
                return _invoiceItemRepository.Get(
                    x => x.UserId == userId,
                    q => q.OrderByDescending(i => i.CreatedAt), 
                    "Invoice,Product"
                );
            }
            if(_authenticationService.IsDeliveryPartner())
            {
                return _invoiceItemRepository.Get(
                    x => x.Invoice.DeliveryPartnerId == userId,
                    q => q.OrderByDescending(i => i.CreatedAt),
                    "Invoice,Product"
                );
            }
            if (_authenticationService.IsAdmin())
            {
                return _invoiceItemRepository.Get(
                    null,
                    q => q.OrderByDescending(i => i.CreatedAt),
                    "Invoice,Product"
                );
            }
            else
            {
                throw new Exception("You are not authorized to view this invoice item");
            }
        }
    }
}
