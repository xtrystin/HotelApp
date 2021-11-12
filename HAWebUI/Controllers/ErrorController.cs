using HAWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWebUI.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        [AllowAnonymous]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var requestedPath = HttpContext.Features.Get<IStatusCodeReExecuteFeature>().OriginalPath;

            _logger.LogWarning("Error occurred. StatusCode: {statusCode}, Path: {requestedPath}", statusCode, requestedPath);

            var error = new ErrorDisplayModel()
            {
                Path = requestedPath,
                Title = statusCode.ToString()
            };

            switch (statusCode)
            {
                case 401:
                    error.Message = "You are not authenticated. Please try to sign out, sign in and try again.";
                    break;
                case 403:
                    error.Message = "You do not have permission to access this page.";
                    break;
                case 404:
                    error.Message = "Resource you requested could not be found";
                    break;
                default:
                    error.Message = "Fatal Exception. Please contact with your administrator.";
                    break;
            }

            return View(error);
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            //Get the exception
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ex = exceptionDetails.Error;

            //Log the exception
            _logger.LogWarning("Error occurred. Path: {Path}, User: {User}, ErrorMessage: {ErrorMessage}", exceptionDetails.Path, User.Identity.Name, ex.Message);

            // Create error display model
            ErrorDisplayModel error = new ErrorDisplayModel();
            error.Path = exceptionDetails.Path;

            if (ex == null)
            {
                error.Title = "Exception is null";
                error.Message = "Fatal Exception. Please contact with your administrator.";
            }
            else
            {
                error.Title = ex.Message;
                if (ex.Message == "Forbidden")
                {
                    error.Message = "You do not have permission to access this page";
                }
                else if (ex.Message == "Unauthorized")
                {
                    error.Message = "You are not authenticated. Please try to sign out, sign in and try again.";
                }
                else
                {
                    error.Message = "Fatal Exception. Please contact with your administrator.";
                }
            }

            return View(error);
        }
    }
}
