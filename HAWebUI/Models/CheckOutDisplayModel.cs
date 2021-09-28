using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HAWebUI.Models
{
    public class CheckOutDisplayModel
    {
        public int Id { get; set; }
        public string CashierId { get; set; }
        public DateTime CheckOutDate { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
    }
}
