﻿using HaWebUI.Library.ApiHelpers;
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
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomEndpoint _roomEndpoint;

        public RoomController(ILogger<RoomController> logger, IRoomEndpoint roomEndpoint)
        {
            _logger = logger;
            _roomEndpoint = roomEndpoint;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var token = await GetToken();

                List<RoomModel> apiRooms = await _roomEndpoint.GetAll(token);

                //  Map rooms to RoomDisplayModel
                List<RoomDisplayModel> displayRooms = MyMapper.MapApiRoomModelToDisplayModel(apiRooms);

                return View(displayRooms);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.GetAll(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = ErrorCreator.CreateApiError(ex);

                return View("ApiError", apiError);
            }
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
            try
            {
                var token = await GetToken();

                var apiRoom = MyMapper.MapDisplayModelToApiRoomModel(displayRoom);

                await _roomEndpoint.CreateRoom(token, apiRoom);

                return Redirect("~/room");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.Create:Post(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = ErrorCreator.CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Get Room by id from db
                var token = await GetToken();

                RoomModel apiRoom = await _roomEndpoint.GetRoomById(token, id);

                // Map room to DisplayModel
                var displayRoom = MyMapper.MapApiRoomModelToDisplayModel(apiRoom);

                return View(displayRoom);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.Edit:Get(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = ErrorCreator.CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(RoomDisplayModel displayRoom)
        {
            try
            {
                var token = await GetToken();

                var apiRoom = MyMapper.MapDisplayModelToApiRoomModel(displayRoom);

                await _roomEndpoint.UpdateRoom(token, apiRoom);

                return Redirect("~/room");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.Edit:Post(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = ErrorCreator.CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Cashier,Admin")]
        public IActionResult Details(int id)
        {
            // Display more info about room: TypeId, TypeName
            // Display all RoomTypes?

            return View("~/room");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Get Room by id from db
                var token = await GetToken();

                RoomModel apiRoom = await _roomEndpoint.GetRoomById(token, id);

                // Map room to DisplayModel
                var displayRoom = MyMapper.MapApiRoomModelToDisplayModel(apiRoom);

                return View(displayRoom);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.Delete:Get(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = ErrorCreator.CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(RoomDisplayModel displayRoom)
        {
            try
            {
                var token = await GetToken();

                var id = displayRoom.Id;

                await _roomEndpoint.DeleteRoom(token, id);

                return Redirect("~/room");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.Delete:Post(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = ErrorCreator.CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }


        private async Task<string> GetToken()
        {
            var output = await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.IdToken);
            if (string.IsNullOrEmpty(output))
            {
                //Todo: Redirect user to error page
                throw new Exception("The access token cannot be found in the authentication ticket. " +
                                                    "Make sure that SaveTokens is set to true in the OIDC options.");
            }

            return output;
        }
    }
}
