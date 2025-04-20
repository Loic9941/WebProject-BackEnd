using Domain;

namespace BLL.DTOs.OutputDTOs
{
    public class InvoiceItemOutputDTO
    {
        public required int Id { get; set; }

        public required int InvoiceId { get; set; }

        public required int? ProductId { get; set; }

        public required string ProductName { get; set; }

        public byte[] ProductImage { get; set; } = [];

        public required string? ClientFullName { get; set; }

        public required decimal UnitPrice { get; set; }

        public required int Quantity { get; set; }

        public required string Status { get; set; }

        public required DateOnly? EstimatedDeliveryDate { get; set; }

        public required DateTime CreatedAt { get; set; }

        public required DateTime? ReadyToBePickedUp { get; set; }

        public required DateTime? PickedUpAt { get; set; }

        public required DateTime? InTransitAt { get; set; }

        public required DateTime? DeliveredAt { get; set; }
    }
}
