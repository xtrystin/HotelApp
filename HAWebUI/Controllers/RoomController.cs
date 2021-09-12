using HaWebUI.Library.ApiHelpers;
using HaWebUI.Library.Models;
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
            var token = await GetToken();
            if (string.IsNullOrEmpty(token))
            {
                //Todo: Redirect user to error page
                throw new InvalidOperationException("The access token cannot be found in the authentication ticket. " +
                                                    "Make sure that SaveTokens is set to true in the OIDC options.");
            }

            try
            {
                List<RoomModel> rooms = await _roomEndpoint.GetAll(token);

                //  Map rooms to RoomDisplayModel
                List<RoomDisplayModel> displayRooms = MapRoomModelToDisplayModel(rooms);

                return View(displayRooms);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("User {User} unsuccessfully tried to access RoomEndpoint.GetAll(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

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

                return View("ApiError", apiError);
            }
        }

        private async Task<string> GetToken() => await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.IdToken);

        private List<RoomDisplayModel> MapRoomModelToDisplayModel(List<RoomModel> rooms)
        {
            List<RoomDisplayModel> output = new List<RoomDisplayModel>();

            foreach (var room in rooms)
            {
                var mappedRoom = new RoomDisplayModel
                {
                    Id = room.Id,
                    Name = room.Name,
                    NormalPrice = room.RoomType.NormalPrice,
                    StudentPrice = room.RoomType.StudentPrice,
                    Capacity = room.RoomType.Capacity,
                    Status = room.Status
                };

                output.Add(mappedRoom);
            }

            return output;
        }
    }
}
