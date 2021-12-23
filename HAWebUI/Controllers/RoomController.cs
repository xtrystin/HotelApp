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
            var token = await GetToken();

            List<RoomModel> apiRooms = await _roomEndpoint.GetAll(token);

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
            var token = await GetToken();

            var apiRoom = MyMapper.MapDisplayModelToApiRoomModel(displayRoom);

            await _roomEndpoint.CreateRoom(token, apiRoom);

            return Redirect("~/room");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            // Get Room by id from db
            var token = await GetToken();

            RoomModel apiRoom = await _roomEndpoint.GetRoomById(token, id);

            // Map room to DisplayModel
            var displayRoom = MyMapper.MapApiRoomModelToDisplayModel(apiRoom);

            return View(displayRoom);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(RoomDisplayModel displayRoom)
        {
            var token = await GetToken();

            var apiRoom = MyMapper.MapDisplayModelToApiRoomModel(displayRoom);

            await _roomEndpoint.UpdateRoom(token, apiRoom);

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
            // Get Room by id from db
            var token = await GetToken();

            RoomModel apiRoom = await _roomEndpoint.GetRoomById(token, id);

            // Map room to DisplayModel
            var displayRoom = MyMapper.MapApiRoomModelToDisplayModel(apiRoom);

            return View(displayRoom);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(RoomDisplayModel displayRoom)
        {
            var token = await GetToken();

            var id = displayRoom.Id;

            await _roomEndpoint.DeleteRoom(token, id);

            return Redirect("~/room");
        }


        private async Task<string> GetToken() => await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.AccessToken);
    }
}
