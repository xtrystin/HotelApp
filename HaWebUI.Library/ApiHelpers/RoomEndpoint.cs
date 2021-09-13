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
    public class RoomEndpoint : IRoomEndpoint
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

        public async Task<RoomModel> GetRoomById(string token, int id)
        {
            var client = _httpClientFactory.CreateClient("HAApiClient");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using var response = await client.GetAsync($"api/room/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<RoomModel>();

                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task UpdateRoom(string token, RoomModel room)
        {
            var client = _httpClientFactory.CreateClient("HAApiClient");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using var response = await client.PutAsJsonAsync("api/room", room);
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
