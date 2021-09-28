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
        private readonly IHttpClientFactory _httpClientFactory;

        public CheckOutEndpoint(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task PostCheckOutInfo(string token, CheckOutModel checkOutInfo)
        {
            var client = _httpClientFactory.CreateClient("HAApiClient");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using var response = await client.PostAsJsonAsync("api/checkout", checkOutInfo);

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
