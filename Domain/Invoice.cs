using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public int ContactId { get; set; }
        public virtual Contact? Contact { get; set; } = null!;

        public List<InvoiceItem> InvoiceItems { get; set; } = [];
    }
}
