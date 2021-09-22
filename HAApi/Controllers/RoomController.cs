using HAApi.Library.DataAccess;
using HAApi.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomData _roomData;

        public RoomController(IRoomData roomData)
        {
            _roomData = roomData;
        }

        // GET: api/<RoomController>
        [HttpGet]
        [Authorize]    //Todo: Change Role to "User/Cashier"
        public List<Room> Get()
        {
            var output = _roomData.GetRooms();
            
            //var UserId = User.Identity.Name;
            
            return output;
        }

        // GET api/<RoomController>/<id>
        [HttpGet("{id}")]
        [Authorize] //Todo: Change Role to "User/Cashier"
        public Room Get(int id)
        {
            var output = _roomData.GetRooms().Where(x => x.Id == id).FirstOrDefault();

            return output;
        }

        // POST api/<RoomController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Post([FromBody] Room room)
        {
            _roomData.AddRoom(room);
        }

        // PUT api/<RoomController>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public void Put([FromBody] Room room)
        {
            _roomData.UpdateRoom(room);
        }

        // DELETE api/<RoomController>/<id>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            _roomData.DeleteRoom(id);
        }
    }
}
