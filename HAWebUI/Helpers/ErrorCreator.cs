using HAWebUI.Models;
using System;

namespace HAWebUI.Helpers
{
    public static class ErrorCreator
    {
        public static ApiErrorDisplayModel CreateApiError(Exception ex)
        {
            ApiErrorDisplayModel apiError = new ApiErrorDisplayModel();

            if (ex == null)
            {
                apiError.Title = "Exception is null";
                apiError.Message = "Fatal Exception. Please contact with your administrator!";

                return apiError;
            }
            else
            {
                apiError.Title = ex.Message;
                if (ex.Message == "Forbidden")
                {
                    apiError.Message = "You do not have permission to access this page";
                }
                else if (ex.Message == "Unauthorized")
                {
                    apiError.Message = "You are not authorized. Please try sign out, sign in and try again.";
                }
                else
                {
                    apiError.Message = "Fatal Exception. Please contact with your administrator!";
                }

                return apiError; 
            }
        }
    }
}
