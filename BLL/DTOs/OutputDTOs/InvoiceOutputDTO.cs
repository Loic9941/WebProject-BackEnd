namespace BLL.DTOs.OutputDTOs
{
    public class InvoiceOutputDTO
    {
        public required int Id { get; set; }

        public IEnumerable<InvoiceItemOutputDTO> InvoiceItems { get; set; } = [];

        public required string Status { get; set; }

        public int? DeliveryPartnerId { get; set; }

        public string? DeliveryPartnerName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public DateTime? PaidAt { get; set; } = null;
    }
}
