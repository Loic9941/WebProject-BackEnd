

using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        
        /*public List<Product> Products { get; set; }*/
    }
}
