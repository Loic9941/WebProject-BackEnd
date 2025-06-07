using BLL.DTOs.InputDTOs;
using BLL.IServices;
using DAL.IRepositories;
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

        public Invoice? GetById(int id)
        {  
            var invoice = _invoiceRepository.GetSingleOrDefault(
                x => x.Id == id,
                "InvoiceItems,InvoiceItems.Product,InvoiceItems.Rating,DeliveryPartner"
                );
            if(
                !_authenticationService.IsAdmin() && 
                invoice is not null && 
                invoice.UserId != _authenticationService.GetUserId())
            {
                throw new Exception("You are not authorized to access this invoice");
            }
            return invoice;
        }

        public IEnumerable<Invoice> Get()
        {
            var userId = _authenticationService.GetUserId() ?? throw new Exception("User not found");
            if (_authenticationService.IsCustomer())
            {
                return _invoiceRepository.Get(x => x.UserId == _authenticationService.GetUserId(), 
                    q => q.OrderByDescending(i => i.CreatedAt), 
                    "InvoiceItems"
                );
            }
            return _invoiceRepository.Get();
        }

        public Invoice? Pending()
        {
            var userId = _authenticationService.GetUserId() ?? throw new Exception("User not found");
            return _invoiceRepository.GetSingleOrDefault(x => x.UserId == userId && x.Status == "pending", "InvoiceItems");
        }

        public void MarkAsPaid(int invoiceId, MarkInvoiceAsPaidDTO markAsPaidDTO)
        {
            Invoice invoiceToMarkAsPaid = _invoiceRepository.GetSingleOrDefault(
                x => x.Id == invoiceId
                ) ?? throw new Exception("Invoice not found");
            invoiceToMarkAsPaid.Status = "paid";
            invoiceToMarkAsPaid.DeliveryPartnerId = markAsPaidDTO.DeliveryPartnerId;
            invoiceToMarkAsPaid.PaidAt = DateTime.Now;
            _invoiceRepository.Update(invoiceToMarkAsPaid);
        }
    }
}
