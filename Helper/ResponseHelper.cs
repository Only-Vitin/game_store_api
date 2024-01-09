using System;
using Microsoft.AspNetCore.Http;

using game_store_api.Interfaces;

namespace game_store_api.Helper
{
    public class ResponseHelper : IResponseHelper
    {
        public void AddDateHeaders(HttpResponse response)
        {
            response.Headers.Add("Date", $"{DateTime.Now}");
        }

        public void AddTokenHeaders(HttpResponse response, string token)
        {
            response.Headers.Add("Authorization", $"Bearer {token}");
        }
    }
}
