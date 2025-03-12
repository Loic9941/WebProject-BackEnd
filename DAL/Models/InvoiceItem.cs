using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        /*public int? InvoiceId { get; set; }
        public virtual Invoice? Invoice { get; set; }
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        */
    }
}
