using BLL.DTOs.OutputDTOs;
using Domain;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api
{
    public static class Mapper
    {
        //Rate
        public static RatingOutputDTO MapToDTO(this Rating rating)
        {
            return new RatingOutputDTO
            {
                Id = rating.Id,
                Rate = rating.Rate,
                Text = rating.Text,
                CreatedAt = rating.CreatedAt,
                FirstName = rating.InvoiceItem.User?.Firstname ?? "",
                LastName = rating.InvoiceItem.User?.Lastname ?? "",
                ProductName = rating.InvoiceItem.Name,
                CommentId = rating.Comment?.Id,
                CommentText = rating.Comment?.Text,
                CommentCreatedAt = rating.Comment?.CreatedAt
            };
        }

        //User
        public static UserOutputDTO MapToDTO(this User user)
        {
            return new UserOutputDTO
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Role = user.Role,
            };
        }

        //comment
        public static CommentOutputDTO MapToDTO(this Comment comment)
        {
            return new CommentOutputDTO
            {
            };
        }

        //Invoice
        public static InvoiceOutputDTO MapToDTO(this Invoice invoice)
        {
            return new InvoiceOutputDTO
            {
                Id = invoice.Id,
                Status = invoice.Status,
                DeliveryPartnerId = invoice.DeliveryPartnerId,
                DeliveryPartnerName = invoice.DeliveryPartner?.Lastname,
                PaidAt = invoice.PaidAt,
                CreatedAt = invoice.CreatedAt,
                InvoiceItems = invoice.InvoiceItems.Select(x => x.MapToDTO())
            };
        }

        //InvoiceItem
        public static InvoiceItemOutputDTO MapToDTO(this InvoiceItem invoiceItem)
        {
            return new InvoiceItemOutputDTO
            {
                Id = invoiceItem.Id,
                InvoiceId = invoiceItem.InvoiceId,
                ProductName = invoiceItem.Name,
                UnitPrice = invoiceItem.UnitPrice,
                Quantity = invoiceItem.Quantity,
                Status = invoiceItem.Status,
                ProductId = invoiceItem.ProductId,
                ClientFullName = invoiceItem.User?.Firstname + ' ' + invoiceItem.User?.Lastname,
                EstimatedDeliveryDate = invoiceItem.EstimatedDeliveryDate,
                CreatedAt = invoiceItem.CreatedAt,
                ReadyToBePickedUp = invoiceItem.ReadyToBePickedUp,
                PickedUpAt = invoiceItem.PickedUpAt,
                InTransitAt = invoiceItem.InTransitAt,
                DeliveredAt = invoiceItem.DeliveredAt,
                ProductImage = invoiceItem.Product is not null ? invoiceItem.Product.Image : []
            };
        }

        //Product
        public static ProductOutputDTO MapToDTO(this Product product)
        {
            return new ProductOutputDTO
            {
                Id = product.Id,
                ArtistFullName = product.User?.Firstname,
                ArtisteId = product.User?.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Category = product.Category,
            };
        }

    }
}
