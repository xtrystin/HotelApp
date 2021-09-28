using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.Models.EFModels
{
    public class CheckIn
    {
        public int Id { get; set; }
        [Required]
        public string CashierId { get; set; }
        [Required]
        public int AdultsAmount { get; set; }
        [Required]
        public int StudentsAmount { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public int DaysAmount { get; set; }

        [Required]
        public int RoomId { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}
