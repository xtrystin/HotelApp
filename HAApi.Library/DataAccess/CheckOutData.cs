using HAApi.Library.Context;
using HAApi.Library.Models;
using HAApi.Library.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.DataAccess
{
    public class CheckOutData : ICheckOutData
    {
        private readonly EFContext _db;

        public CheckOutData(EFContext db)
        {
            _db = db;
        }

        public void SaveCheckOutInfo(CheckOutDtoModel checkOutInfo)
        {
            var checkOutEFModel = new CheckOut
            {
                Id = checkOutInfo.Id,
                CashierId = checkOutInfo.CashierId,
                CheckOutDate = checkOutInfo.CheckOutDate
            };

            var room = FindRoom(checkOutInfo.RoomId);
            var customer = FindCustomer(checkOutInfo.FirstName, checkOutInfo.LastName, checkOutInfo.Phone);
            EnsureAreValid(room, customer);    //Todo Check if this customer has been in this room

            room.Status = "empty";

            checkOutEFModel.Room = room;
            checkOutEFModel.Customer = customer;

            _db.CheckOuts.Add(checkOutEFModel);

            _db.SaveChanges();
        }

        private Room FindRoom(int id) => _db.Rooms.FirstOrDefault(x => x.Id == id);

        private Customer FindCustomer(string firstName, string lastName, string phone)
            => _db.Customers.FirstOrDefault(x => x.FirstName == firstName
               && x.LastName == lastName
               && x.Phone == phone);
    
        private void EnsureAreValid(Room room, Customer customer)
        {
            if (room == null || customer == null)
            {
                throw new Exception("Room or customer not found");
            }
            else if (room.Status == "empty")
            {
                throw new Exception("The room is already empty");
            }
        }
    }
}
