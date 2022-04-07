using HaWebUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public class CheckOutEndpoint : ICheckOutEndpoint
    {
        private readonly IApiHelper _apiHelper;

        public CheckOutEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostCheckOutInfo(CheckOutModel checkOutInfo)
        {
            using var response = await _apiHelper.ApiClient.PostAsJsonAsync("api/checkout", checkOutInfo);

            if (response.IsSuccessStatusCode)
            {
                // Log success
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
