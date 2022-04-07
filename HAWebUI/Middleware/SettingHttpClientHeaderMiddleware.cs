using HaWebUI.Library.ApiHelpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWebUI.Middleware
{
    public class SettingHttpClientHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiHelper _apiHelper;

        public SettingHttpClientHeaderMiddleware(RequestDelegate next, IApiHelper apiHelper)
        {
            _next = next;
            _apiHelper = apiHelper;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                 // Save token to the HttpClient header, so that we do not have to input a token each time before calling api
                var token = await context.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.AccessToken);

                _apiHelper.SetTokenInHeader(token);
            }
            else
            {
                _apiHelper.DeleteTokenFromHeader();
            }

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }

    public static class SettingHttpClienHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseSettingHttpClientHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SettingHttpClientHeaderMiddleware>();
        }
    }
}
