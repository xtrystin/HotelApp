using HaWebUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public class CheckInEndpoint : ICheckInEndpoint
    {
        private readonly IApiHelper _apiHelper;

        public CheckInEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task<PaymentModel> PostCheckInInfo(CheckInModel checkInInfo)
        {
            using var response = await _apiHelper.ApiClient.PostAsJsonAsync("api/checkIn", checkInInfo);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<PaymentModel>();

                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task DeleteLastCheckInCashierMade(string cashierId)
        {
            using var response = await _apiHelper.ApiClient.PostAsJsonAsync($"api/checkIn/DeleteLastCheckIn", cashierId);
            
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
