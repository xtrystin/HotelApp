using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public class ApiHelper : IApiHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _apiClient;

        public ApiHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiClient = _httpClientFactory.CreateClient("HAApiClient");
        }

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }

        public void SetTokenInHeader(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appliacation/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public void DeleteTokenFromHeader()
        {
            _apiClient.DefaultRequestHeaders.Clear();
        }
    }
}
