using HaWebUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public class RoomEndpoint
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoomEndpoint(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<RoomModel>> GetAll(string token)
        {
            var client = _httpClientFactory.CreateClient("HAApiClient");

            using var request = new HttpRequestMessage(HttpMethod.Get, "api/room");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                //var result = JsonSerializer.DeserializeAsync<List<RoomModel>>(await response.Content.ReadAsStreamAsync()).Result;
                var result = await response.Content.ReadAsAsync<List<RoomModel>>();

                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
