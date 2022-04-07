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
        private readonly IApiHelper _apiHelper;

        public RoomEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<RoomModel>> GetAll()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "api/room");

            using var response = await _apiHelper.ApiClient.SendAsync(request);
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

        public async Task<RoomModel> GetRoomById(int id)
        {
            using var response = await _apiHelper.ApiClient.GetAsync($"api/room/{id}");
            
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

        public async Task CreateRoom(RoomModel room)
        {
            using var response = await _apiHelper.ApiClient.PostAsJsonAsync("api/room", room);
            
            if (response.IsSuccessStatusCode)
            {
                // Log success
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task UpdateRoom(RoomModel room)
        {
            using var response = await _apiHelper.ApiClient.PutAsJsonAsync("api/room", room);
            
            if (response.IsSuccessStatusCode)
            {
                // Log success
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task DeleteRoom(int id)
        {
            using var response = await _apiHelper.ApiClient.DeleteAsync($"api/room/{id}");
            
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
