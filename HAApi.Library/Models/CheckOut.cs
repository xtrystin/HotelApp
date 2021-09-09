using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.Models
{
    public class CheckOut
    {
        public int Id { get; set; }
        public int CashierId { get; set; }
        public DateTime CheckOutDate { get; set; } = DateTime.Now;

        [Required]
        public Room Room { get; set; }
        [Required]
        public Customer Customer { get; set; }
    }
}
