using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal SubTotal { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal Tax { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal Total { get; set; }
    }
}
