using HaWebUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HAWebUI.Models
{
    public class CheckInDisplayModel
    {
        public int Id { get; set; }
        [Required]
        public string CashierId { get; set; }

        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public int RoomId { get; set; }
        [Required]
        public int AdultsAmount { get; set; }
        [Required]
        public int StudentsAmount { get; set; }
        public DateTime CheckInDate { get; set; }
        [Required]
        public int DaysAmount { get; set; }
    }
}
