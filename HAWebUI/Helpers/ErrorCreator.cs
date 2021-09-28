using HAWebUI.Models;
using System;

namespace HAWebUI.Helpers
{
    public static class ErrorCreator
    {
        public static GeneralErrorDisplayModel CreateGeneralError(Exception ex)
        {
            GeneralErrorDisplayModel error = new GeneralErrorDisplayModel();

            if (ex == null)
            {
                error.Title = "Exception is null";
                error.Message = "Fatal Exception. Please contact with your administrator!";

                return error;
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
                    error.Message = "You are not authorized. Please try to sign out, sign in and try again.";
                }
                else
                {
                    error.Message = "Fatal Exception. Please contact with your administrator!";
                }

                return error; 
            }
        }
    }
}
