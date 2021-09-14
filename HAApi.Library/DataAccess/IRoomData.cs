using HAApi.Library.Models;
using System.Collections.Generic;

namespace HAApi.Library.DataAccess
{
    public interface IRoomData
    {
        List<Room> GetRooms();
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
    }
}