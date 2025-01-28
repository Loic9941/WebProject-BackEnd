using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Stars { get; set; }

        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
