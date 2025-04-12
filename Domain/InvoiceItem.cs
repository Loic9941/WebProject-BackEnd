using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        
        public int InvoiceId { get; set; }
        public virtual Invoice? Invoice { get; set; } = null!;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        // inPreparation ,pickedUp, inTransit, delivered
        public string Status { get; set; } = null!;

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? InPreparationAt { get; set; }

        public DateTime? PickedUpAt { get; set; }

        public DateTime? InTransitAt { get; set; }

        public DateTime? DeliveredAt { get; set; }
    }
}
