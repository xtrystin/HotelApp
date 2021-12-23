using HaWebUI.Library.ApiHelpers;
using HaWebUI.Library.Models;
using HAWebUI.Helpers;
using HAWebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWebUI.Controllers
{
    public class CheckInController : Controller
    {
        private readonly ICheckInEndpoint _checkInEndpoint;
        private readonly IRoomEndpoint _roomEndpoint;

        public CheckInController(ICheckInEndpoint checkInEndpoint, IRoomEndpoint roomEndpoint)
        {
            _checkInEndpoint = checkInEndpoint;
            _roomEndpoint = roomEndpoint;
        }

        // GET: CheckInController
        [Authorize(Roles = "Cashier,Admin")]
        [HttpGet]
        public ActionResult Index()
        {
            var displayCheckInInfo = new CheckInDisplayModel();

            return View(displayCheckInInfo);
        }

        [Authorize(Roles = "Cashier,Admin")]
        [HttpPost]
        public async Task<ActionResult> Index(CheckInDisplayModel displayCheckInInfo)
        {
            // Fill in addictional information
            displayCheckInInfo.CashierId = User.Identity.Name;
            displayCheckInInfo.CheckInDate = DateTime.Now;

            var token = await GetToken();

            //  Check room status, capacity
            await EnsureRoomAvailability(token, displayCheckInInfo.RoomId, (displayCheckInInfo.StudentsAmount + displayCheckInInfo.AdultsAmount));
               
            // Map displayModel to api model
            var apiCheckInInfo = MyMapper.MapDisplayModelToApiCheckInModel(displayCheckInInfo);

            // Send to api & get payment calculations
            var paymentInfo = await _checkInEndpoint.PostCheckInInfo(token, apiCheckInInfo);

            // Display paymentInfo on Summary Page
            return View("Summary", paymentInfo);
        }

        [Authorize(Roles = "Cashier,Admin")]
        public async Task<ActionResult> Cancel()
        {
            var token = await GetToken();
            var cashierId = User.Identity.Name;

            // Remove created CheckIn record from DB
            await _checkInEndpoint.DeleteLastCheckInCashierMade(token, cashierId);

            // Todo: Inform user about successful result
            return Redirect("~/home");
        }


        private async Task<string> GetToken() => await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.AccessToken);

        private async Task EnsureRoomAvailability(string token, int roomId, int peopleAmount)
        {
            var selectedRoom = await _roomEndpoint.GetRoomById(token, roomId);
            if (selectedRoom.Status != "empty" || peopleAmount > selectedRoom.Capacity)
            {
                throw new Exception("Room is not available (room is not empty or is too small)");
            }
        }
    }
}
