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

                List<RoomModel> rooms = await _roomEndpoint.GetAll(token);

                //  Map rooms to RoomDisplayModel
                List<RoomDisplayModel> displayRooms = MyMapper.MapRoomModelToDisplayModel(rooms);

                return View(displayRooms);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.GetAll(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }

        public IActionResult Create()
        {
            // Create new Room
            
            // Save it to DB

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Get Room by id from db
                var token = await GetToken();

                RoomModel room = await _roomEndpoint.GetRoomById(token, id);

                // Map room to DisplayModel
                var output = MyMapper.MapRoomModelToDisplayModel(room);

                return View(output);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.Edit:Get(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(RoomDisplayModel room)
        {
            try
            {
                var token = await GetToken();

                var apiRoom = MyMapper.MapDisplayModelToApiModel(room);

                await _roomEndpoint.UpdateRoom(token, apiRoom);

                return Redirect("~/room");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access RoomEndpoint.Edit:Post(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }

        public IActionResult Details(int id)
        {
            // Display more info about room: TypeId, TypeName
            // Display all RoomTypes?

            return View();
        }

        public IActionResult Delete(int id)
        {


            return View();
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

        // Todo: ? Move it to Helpers.ErrorCreator ?
        public ApiErrorDisplayModel CreateApiError(Exception ex)
        {
            ApiErrorDisplayModel apiError = new ApiErrorDisplayModel();
            apiError.Title = ex.Message;

            if (ex.Message == "Forbidden")
            {
                apiError.Message = "You do not have permission to access this page";
            }
            else
            {
                apiError.Message = "Fatal Exception";
            }

            return apiError;
        }


    }
}
