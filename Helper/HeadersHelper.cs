using System;
using Microsoft.AspNetCore.Http;

namespace game_store_api.Helper
{
    public class HeadersHelper
    {
        public static void AddDateOnHeaders(HttpResponse response)
        {
            response.Headers.Add("Date", $"{DateTime.Now}");      
        }

        public static void AddAuthorizationOnHeaders(HttpResponse response, string token)
        {
            response.Headers.Add("Authorization", $"Bearer {token}");
        }
    }
}
