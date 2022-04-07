using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HAWebUI.Models;
using HAWebUI.Helpers;
using HaWebUI.Library.ApiHelpers;

namespace HAWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiHelper _apiHelper;

        public HomeController(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task <IActionResult> Index()
        {
            return View();
        }

        [HttpGet("~/privacy")]
        public async Task<IActionResult> Privacy()
        {
            var token = await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.AccessToken);
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("The access token cannot be found in the authentication ticket. " +
                                                    "Make sure that SaveTokens is set to true in the OIDC options.");
            }

            using var client = new HttpClient();

            using var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44313/api/message");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return View();
        }

    }
}
