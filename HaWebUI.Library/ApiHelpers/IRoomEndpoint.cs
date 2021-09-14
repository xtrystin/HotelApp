using HaWebUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public interface IRoomEndpoint
    {
        Task<List<RoomModel>> GetAll(string token);
        Task<RoomModel> GetRoomById(string token, int id);
        Task CreateRoom(string token, RoomModel room);
        Task UpdateRoom(string token, RoomModel room);
    }
}