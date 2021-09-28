using HaWebUI.Library.Models;
using HAWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWebUI.Helpers
{
    public static class MyMapper
    {
        //Todo: ? make generic func / use AutoMapper ?
        public static List<RoomDisplayModel> MapApiRoomModelToDisplayModel(List<RoomModel> rooms)
        {
            if(rooms == null)
            {
                throw new Exception("Mapper failed: The list of rooms is empty");
            }

            List<RoomDisplayModel> output = new List<RoomDisplayModel>();

            foreach (var room in rooms)
            {
                var mappedRoom = new RoomDisplayModel
                {
                    Id = room.Id,
                    Name = room.Name,
                    NormalPrice = room.NormalPrice,
                    StudentPrice = room.StudentPrice,
                    Capacity = room.Capacity,
                    Status = room.Status
                };

                output.Add(mappedRoom);
            }

            return output;
        }

        public static RoomDisplayModel MapApiRoomModelToDisplayModel(RoomModel room)
        {
            if (room == null)
            {
                throw new Exception("Mapper failed: The room is empty");
            }

            var output = new RoomDisplayModel
            {
                Id = room.Id,
                Name = room.Name,
                NormalPrice = room.NormalPrice,
                StudentPrice = room.StudentPrice,
                Capacity = room.Capacity,
                Status = room.Status
            };

            return output;
        }

        public static RoomModel MapDisplayModelToApiRoomModel(RoomDisplayModel room)
        {
            if (room == null)
            {
                throw new Exception("Mapper failed: The room is empty");
            }

            var output = new RoomModel()
            {
                Id = room.Id,
                Name = room.Name,
                Status = room.Status,
                NormalPrice = room.NormalPrice,
                StudentPrice = room.StudentPrice,
                Capacity = room.Capacity
            };

            return output;
        }

        // Todo: Test it
        public static CheckInModel MapDisplayModelToApiCheckInModel(CheckInDisplayModel checkInInfo)
        {
            if(checkInInfo == null)
            {
                throw new Exception("Mapper failed: The checkIn info is empty");
            }

            var output = new CheckInModel
            {
                Id = checkInInfo.Id,
                CashierId = checkInInfo.CashierId,
                AdultsAmount = checkInInfo.AdultsAmount,
                StudentsAmount = checkInInfo.StudentsAmount,
                CheckInDate = checkInInfo.CheckInDate,
                DaysAmount = checkInInfo.DaysAmount,
                RoomId = checkInInfo.RoomId,
                Customer = new CustomerModel
                {
                    Id = checkInInfo.CustomerId,
                    FirstName = checkInInfo.FirstName,
                    LastName = checkInInfo.LastName,
                    Email = checkInInfo.Email,
                    Phone = checkInInfo.Phone
                }
            };

            return output;
        }

        // Todo: Test it
        public static CheckOutModel MapDisplayModelToApiCheckOutModel(CheckOutDisplayModel checkOutInfo)
        {
            if (checkOutInfo == null)
            {
                throw new Exception("Mapper failed: The checkOutInfo is empty");
            }

            var output = new CheckOutModel
            {
                Id = checkOutInfo.Id,
                CashierId = checkOutInfo.CashierId,
                CheckOutDate = checkOutInfo.CheckOutDate,
                RoomId = checkOutInfo.RoomId,
                FirstName = checkOutInfo.FirstName,
                LastName = checkOutInfo.LastName,
                Phone = checkOutInfo.Phone
            };

            return output;
        }
    }
}
