using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        /*public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        */
    }
}
