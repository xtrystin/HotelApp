using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.Models.DtoModels
{
    public class CheckOutDtoModel
    {
        public int Id { get; set; }
        [Required]
        public string CashierId { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public int RoomId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
