using HAApi.Library.Context;
using HAApi.Library.Helpers;
using HAApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.DataAccess
{
    public class CheckInData : ICheckInData
    {
        private readonly EFContext _db;
        private readonly IRoomData _roomData;
        private readonly IConfigHelper _configHelper;

        public CheckInData(EFContext db, IRoomData roomData, IConfigHelper configHelper)
        {
            _db = db;
            _roomData = roomData;
            _configHelper = configHelper;
        }

        public Payment SaveCheckInInfo(CheckIn checkInInfo)
        {
            // Get Room price from DB
            var room = _roomData.GetRooms().FirstOrDefault(x => x.Id == checkInInfo.RoomId);

            int peopleAmount = checkInInfo.AdultsAmount + checkInInfo.StudentsAmount;

            // Validate room status & capacity
            if (room.Status == "empty" && peopleAmount <= room.Capacity)
            {
                room.Status = "taken";
            }
            else
            {
                throw new Exception("Room is not available (room is not empty or is too small)");
            }

            // Calculate Subtotal
            Payment payment = new Payment();

            decimal subTotal = checkInInfo.DaysAmount * 
                ( (checkInInfo.AdultsAmount * room.NormalPrice) 
                + (checkInInfo.StudentsAmount * room.StudentPrice) );
            payment.SubTotal = subTotal;
           
            // Get Tax Rate from Config
            var taxRate = _configHelper.GetTaxRate();

            // Calculate Tax & Total
            payment.Tax = payment.SubTotal * taxRate;
            payment.Total = payment.SubTotal + payment.Tax;

            // Add Payment to CheckIn
            checkInInfo.Payment = payment;

            // Save CheckInInfo to DB
            _db.CheckIns.Add(checkInInfo);

            _db.SaveChanges();

            // Return created Payment
            return payment;
        }

        public void DeleteLastCheckInCashierMade(string cashierId)
        {
            var entry = _db.CheckIns
                .OrderByDescending(a => a.CheckInDate)
                .FirstOrDefault(a => a.CashierId == cashierId);

            if(entry == null)
            {
                throw new Exception("CheckIn not found");
            }

            // Cannot delete entry older than 2 hours
            if (entry.CheckInDate.Hour == DateTime.Now.Hour || entry.CheckInDate.Hour == DateTime.Now.AddHours(-1).Hour)
            {
                _db.CheckIns.Remove(entry);

                _db.SaveChanges();
            }
            else
            {
                throw new Exception("Cannot delete CheckIn older than 1 hour");
            }

        }
    }
}
