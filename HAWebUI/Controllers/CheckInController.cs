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
        private readonly ILogger<CheckInController> _logger;
        private readonly ICheckInEndpoint _checkInEndpoint;
        private readonly IRoomEndpoint _roomEndpoint;

        public CheckInController(ILogger<CheckInController> logger, ICheckInEndpoint checkInEndpoint, IRoomEndpoint roomEndpoint)
        {
            _logger = logger;
            _checkInEndpoint = checkInEndpoint;
            _roomEndpoint = roomEndpoint;
        }

        // GET: CheckInController
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var displayCheckInInfo = new CheckInDisplayModel();

            return View(displayCheckInInfo);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(CheckInDisplayModel displayCheckInInfo)
        {
            // Fill in addictional information
            displayCheckInInfo.CashierId = User.Identity.Name;
            displayCheckInInfo.CheckInDate = DateTime.Now;

            try
            {
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
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access CheckInController.Index:Post(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                // Todo: Make it more generic (not only api error can occur)
                var apiError = ErrorCreator.CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }

        [Authorize]
        public async Task<ActionResult> Cancel()
        {
            try
            {
                var token = await GetToken();
                var cashierId = User.Identity.Name;

                // Remove created CheckIn record from DB
                await _checkInEndpoint.DeleteLastCheckInCashierMade(token, cashierId);

                // Todo: Inform user about successful result(through apiError page?)
                return Redirect("~/home");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access CheckInController.Cancel. Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var apiError = ErrorCreator.CreateApiError(ex);

                return View("ApiError", apiError);
            }
        }

        //Todo: Make it DRY (same function in RoomController and here)
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
