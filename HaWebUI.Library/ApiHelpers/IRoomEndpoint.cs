using HaWebUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public interface IRoomEndpoint
    {
        Task<List<RoomModel>> GetAll(string token);
    }
}