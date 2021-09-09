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
        [Authorize(Roles = "Admin")]
        public List<Room> Get()
        {
            var output = _roomData.GetRooms();
            
            //var UserId = User.Identity.Name;
            
            return output;
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoomController>
        [HttpPost]
        public void Post([FromBody] Room room)
        {
            _roomData.AddRoom(room);
        }

        // PUT api/<RoomController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
