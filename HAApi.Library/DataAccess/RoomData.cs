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
                .Include(a => a.RoomType)
                .ToList();

            return output;
        }

        public void AddRoom(Room room)
        {
            _db.Rooms.Add(room);

            _db.SaveChanges();
        }
    }
}
