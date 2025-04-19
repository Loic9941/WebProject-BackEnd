using BLL.DTOs;
using BLL.IService;
using DAL.Repository;
using Domain;
using Errors;

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
            if (_authenticationService.IsDeliveryPartner())
            {
                var forDeliveryPartnerStatuses = new List<string> { 
                    "readyToBePickedUp", 
                    "pickedUp", 
                    "inTransit", 
                    "delivered"
                };
                return _invoiceItemRepository.Get(
                    x => x.Invoice.DeliveryPartnerId == userId && 
                    forDeliveryPartnerStatuses.Contains(x.Status),
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

        public InvoiceItem? MarkAsReadyToBeShipped(int id)
        {
            var invoiceItem = _invoiceItemRepository.GetSingleOrDefault(x => x.Id == id) ?? throw new Exception("Invoice item not found");
            if (invoiceItem.Status != "inPreparation")
            {
                throw new Exception("Invoice item is not in preparation");
            }
            invoiceItem.Status = "readyToBePickedUp";
            invoiceItem.ReadyToBePickedUp = DateTime.Now;
            _invoiceItemRepository.Update(invoiceItem);
            return invoiceItem;
        }

        public InvoiceItem? MarkAsPickedUp(int id, MarkInvoiceItemAsDTO markInvoiceItemAsDTO)
        {
            var invoiceItem = _invoiceItemRepository.GetSingleOrDefault(x => x.Id == id) ?? throw new Exception("Invoice item not found");
            if (invoiceItem.Status != "readyToBePickedUp")
            {
                throw new Exception("Invoice item is not ready to be picked up");
            }
            invoiceItem.Status = "pickedUp";
            invoiceItem.PickedUpAt = DateTime.Now;
            if (markInvoiceItemAsDTO.EstimatedDeliveryDate != null)
            {
                invoiceItem.EstimatedDeliveryDate = markInvoiceItemAsDTO.EstimatedDeliveryDate;
            }   
            _invoiceItemRepository.Update(invoiceItem);
            return invoiceItem;
        }

        public InvoiceItem? MarkAsInTransit(int id, MarkInvoiceItemAsDTO markInvoiceItemAsDTO)
        {
            var invoiceItem = _invoiceItemRepository.GetSingleOrDefault(x => x.Id == id) ?? throw new Exception("Invoice item not found");
            if (invoiceItem.Status != "pickedUp")
            {
                throw new Exception("Invoice item is not ready to be in transit");
            }
            invoiceItem.Status = "inTransit";
            invoiceItem.InTransitAt = DateTime.Now;
            if (markInvoiceItemAsDTO.EstimatedDeliveryDate != null)
            {
                invoiceItem.EstimatedDeliveryDate = markInvoiceItemAsDTO.EstimatedDeliveryDate;
            }
            _invoiceItemRepository.Update(invoiceItem);
            return invoiceItem;
        }

        public InvoiceItem? MarkAsDelivered(int id)
        {
            var invoiceItem = _invoiceItemRepository.GetSingleOrDefault(x => x.Id == id) ?? throw new Exception("Invoice item not found");
            if (invoiceItem.Status != "inTransit")
            {
                throw new Exception("Invoice item is not ready to be delivered");
            }
            invoiceItem.Status = "delivered";
            invoiceItem.DeliveredAt = DateTime.Now;
            _invoiceItemRepository.Update(invoiceItem);
            return invoiceItem;
        }

        public void Rate(int id, RateProductDTO rateProductDTO)
        {
            InvoiceItem? invoiceItem = _invoiceItemRepository.GetSingleOrDefault(x => x.Id == id,"Rating") ??
                throw new Exception("InvoiceItem not found");
            if (invoiceItem.Rating is not null)
            {
                throw new RatingConflict();
            }
            invoiceItem.Rating = new Rating
            {
                Rate = rateProductDTO.Rate,
                Text = rateProductDTO.Text,
                InvoiceItemId = id
            };
            _invoiceItemRepository.Update(invoiceItem);
        }
    }
}
