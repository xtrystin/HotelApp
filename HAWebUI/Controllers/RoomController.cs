using HaWebUI.Library.ApiHelpers;
using HaWebUI.Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HAWebUI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoomController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var token = await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.IdToken);
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("The access token cannot be found in the authentication ticket. " +
                                                    "Make sure that SaveTokens is set to true in the OIDC options.");
            }

            //Todo: Use DI
            RoomEndpoint rmEndpoint = new RoomEndpoint(_httpClientFactory);
            try
            {
                List<RoomModel> rooms = await rmEndpoint.GetAll(token);

                // Todo: Map rooms to RoomDisplayModel

                return View(rooms);
            }
            catch (Exception ex)
            {
                //Todo: Log this ex and display error

                return View("~/home");
            }

            //using var client = new HttpClient();

            //using var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/api/room");
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //using var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();

        }
    }
}
