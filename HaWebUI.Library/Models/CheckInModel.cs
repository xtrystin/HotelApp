using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaWebUI.Library.Models
{
    public class CheckInModel
    {
        public int Id { get; set; }
        public string CashierId { get; set; }
        public int AdultsAmount { get; set; }
        public int StudentsAmount { get; set; }
        public DateTime CheckInDate { get; set; }
        public int DaysAmount { get; set; }

        public int RoomId { get; set; }
        public CustomerModel Customer { get; set; }
        public PaymentModel Payment { get; set; }
    }
}
