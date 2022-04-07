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
        private readonly ICheckOutEndpoint _checkOutEndpoint;

        public CheckOutController( ICheckOutEndpoint checkOutEndpoint)
        {
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

            // Map displayModel to api model
            var apiCheckOutInfo = MyMapper.MapDisplayModelToApiCheckOutModel(displayCheckOutInfo);

            // Send to api 
            await _checkOutEndpoint.PostCheckOutInfo(apiCheckOutInfo);

            // Todo: Display success Page
            return Redirect("~/home");
        }
    }
}
