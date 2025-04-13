using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; } = null!;

        public required int? ProductId { get; set; }

        public Product? Product { get; set; } = null!;

        //to allow to delete a product
        public required string Name { get; set; }

        [Required]
        public required int UserId { get; set; }
        public virtual User? User { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        // inPreparation, readyToBePickedUp ,pickedUp, inTransit, delivered
        public string Status { get; set; } = "inPreparation";

        public DateOnly? EstimatedDeliveryDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReadyToBePickedUp { get; set; }

        public DateTime? PickedUpAt { get; set; }

        public DateTime? InTransitAt { get; set; }

        public DateTime? DeliveredAt { get; set; }
    }
}
