using HaWebUI.Library.ApiHelpers;
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
    public class CheckOutController : Controller
    {
        private readonly ILogger<CheckOutController> _logger;
        private readonly ICheckOutEndpoint _checkOutEndpoint;

        public CheckOutController(ILogger<CheckOutController> logger, ICheckOutEndpoint checkOutEndpoint)
        {
            _logger = logger;
            _checkOutEndpoint = checkOutEndpoint;
        }

        // GET: CheckOutController
        [Authorize(Roles = "Cashier,Admin")]
        [HttpGet]
        public ActionResult Index()
        {
            var displayCheckOutInfo = new CheckOutDisplayModel();

            return View(displayCheckOutInfo);
        }

        [Authorize(Roles = "Cashier,Admin")]
        [HttpPost]
        public async Task<ActionResult> Index(CheckOutDisplayModel displayCheckOutInfo)
        {
            // Fill in addictional information
            displayCheckOutInfo.CashierId = User.Identity.Name;
            displayCheckOutInfo.CheckOutDate = DateTime.Now;

            try
            {
                var token = await GetToken();

                // Map displayModel to api model
                var apiCheckOutInfo = MyMapper.MapDisplayModelToApiCheckOutModel(displayCheckOutInfo);

                // Send to api 
                await _checkOutEndpoint.PostCheckOutInfo(token, apiCheckOutInfo);

                // Todo: Display success Page
                return Redirect("~/home");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("User {User} unsuccessfully tried to access CheckOutController.Index:Post(). Exception Message: {ex.Message}", User.Identity.Name, ex.Message);

                var error = ErrorCreator.CreateGeneralError(ex);

                return View("GeneralError", error);
            }
        }

        //Todo: Make it DRY (same function in RoomController and here)
        private async Task<string> GetToken()
        {
            var output = await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.IdToken);
            if (string.IsNullOrEmpty(output))
            {
                throw new Exception("The access token cannot be found in the authentication ticket. " +
                                                    "Make sure that SaveTokens is set to true in the OIDC options.");
            }

            return output;
        }
    }
}
