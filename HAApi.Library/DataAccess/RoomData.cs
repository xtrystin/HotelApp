using HAApi.Library.Context;
using HAApi.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.DataAccess
{
    public class RoomData : IRoomData
    {
        private readonly EFContext _db;

        public RoomData(EFContext db)
        {
            _db = db;
        }

        public List<Room> GetRooms()
        {
            var output = _db.Rooms
                .ToList();

            return output;
        }

        public void AddRoom(Room room)
        {
            _db.Rooms.Add(room);

            _db.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            var entity = _db.Rooms
                .FirstOrDefault(a => a.Id == room.Id);
            if(entity == null)
            {
                throw new Exception("Room not found");
            }
            else
            {
                // Make changes on entity
                entity.Name = room.Name;
                entity.Status = room.Status;
                entity.NormalPrice = room.NormalPrice;
                entity.StudentPrice = room.StudentPrice;
                entity.Capacity = room.Capacity;

                _db.SaveChanges();
            }
        }

        public void DeleteRoom(int id)
        {
            var entity = _db.Rooms.FirstOrDefault(a => a.Id == id);
            
            _db.Rooms.Remove(entity);

            _db.SaveChanges();
        }
    }
}
