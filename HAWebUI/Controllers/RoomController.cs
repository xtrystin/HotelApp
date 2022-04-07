using HaWebUI.Library.ApiHelpers;
using HaWebUI.Library.Models;
using HAWebUI.Helpers;
using HAWebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HAWebUI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomEndpoint _roomEndpoint;

        public RoomController(IRoomEndpoint roomEndpoint)
        {
            _roomEndpoint = roomEndpoint;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<RoomModel> apiRooms = await _roomEndpoint.GetAll();

            //  Map rooms to RoomDisplayModel
            List<RoomDisplayModel> displayRooms = MyMapper.MapApiRoomModelToDisplayModel(apiRooms);

            return View(displayRooms);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            RoomDisplayModel displayRoom = new RoomDisplayModel();

            return View(displayRoom);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RoomDisplayModel displayRoom)
        {
            var apiRoom = MyMapper.MapDisplayModelToApiRoomModel(displayRoom);

            await _roomEndpoint.CreateRoom(apiRoom);

            return Redirect("~/room");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            RoomModel apiRoom = await _roomEndpoint.GetRoomById(id);

            // Map room to DisplayModel
            var displayRoom = MyMapper.MapApiRoomModelToDisplayModel(apiRoom);

            return View(displayRoom);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(RoomDisplayModel displayRoom)
        {
            var apiRoom = MyMapper.MapDisplayModelToApiRoomModel(displayRoom);

            await _roomEndpoint.UpdateRoom(apiRoom);

            return Redirect("~/room");
        }
        [HttpGet]
        [Authorize(Roles = "Cashier,Admin")]
        public IActionResult Details(int id)
        {
            // Todo: Display more info about room: TypeId, TypeName
            // Display all RoomTypes?

            return View("~/room");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            RoomModel apiRoom = await _roomEndpoint.GetRoomById(id);

            // Map room to DisplayModel
            var displayRoom = MyMapper.MapApiRoomModelToDisplayModel(apiRoom);

            return View(displayRoom);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(RoomDisplayModel displayRoom)
        {
            var id = displayRoom.Id;

            await _roomEndpoint.DeleteRoom(id);

            return Redirect("~/room");
        }
    }
}
