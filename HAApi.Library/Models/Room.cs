using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal NormalPrice { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal StudentPrice { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
