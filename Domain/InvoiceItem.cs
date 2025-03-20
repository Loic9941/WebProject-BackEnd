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

        public virtual Product? Product { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
    }
}
