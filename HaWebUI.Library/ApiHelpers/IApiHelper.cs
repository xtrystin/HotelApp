using System.Net.Http;

namespace HaWebUI.Library.ApiHelpers
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }

        void DeleteTokenFromHeader();
        void SetTokenInHeader(string token);
    }
}