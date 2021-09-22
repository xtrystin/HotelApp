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
        private readonly IHttpClientFactory _httpClientFactory;

        public CheckInEndpoint(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<PaymentModel> PostCheckInInfo(string token, CheckInModel checkInInfo)
        {
            var client = _httpClientFactory.CreateClient("HAApiClient");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using var response = await client.PostAsJsonAsync("api/checkIn", checkInInfo);
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

        public async Task DeleteLastCheckInCashierMade(string token, string cashierId)
        {
            var client = _httpClientFactory.CreateClient("HAApiClient");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using var response = await client.PostAsJsonAsync($"api/checkIn/DeleteLastCheckIn", cashierId);
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
