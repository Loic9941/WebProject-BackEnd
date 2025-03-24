using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; } = null!;

        public virtual List<InvoiceItem>? InvoiceItems { get; set; } = [];

        //Possible status : "Pending", "Paid", "Shipped", "Delivered"
        public string Status { get; set; } = "Pending";

        public int? DeliveryPartnerId { get; set; }
        public virtual User? DeliveryPartner { get; set; } = null!;

    }
}
