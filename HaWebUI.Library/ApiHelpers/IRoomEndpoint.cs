using HaWebUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public interface IRoomEndpoint
    {
        Task<List<RoomModel>> GetAll();
        Task<RoomModel> GetRoomById(int id);
        Task CreateRoom(RoomModel room);
        Task UpdateRoom(RoomModel room);
        Task DeleteRoom(int id);
    }
}