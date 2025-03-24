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
    }
}
