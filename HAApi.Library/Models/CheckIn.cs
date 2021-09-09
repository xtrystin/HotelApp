using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.Models
{
    public class CheckIn
    {
        public int Id { get; set; }
        public int CashierId { get; set; }
        [Required]
        public int AdultsAmount{ get; set; }
        public int KidsAmount{ get; set; }
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        [Required]
        public int DaysAmount { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal SubTotal { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal Tax { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        [Required]
        public Room Room { get; set; }
        [Required]
        public Customer Customer { get; set; }
    }
}
