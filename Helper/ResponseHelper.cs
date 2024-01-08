using System;
using Microsoft.AspNetCore.Mvc;

using game_store_api.Interfaces;

namespace game_store_api.Helper
{
    public class ResponseHelper : Controller, IResponseHelper
    {
        public string BodyMessage { get; set; }

        public void AddDateHeaders()
        {
            Response.Headers.Add("Date", $"{DateTime.Now}");
        }
    }
}