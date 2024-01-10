using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Data;
using game_store_api.Helper;
using game_store_api.Services;
using game_store_api.Entities;

namespace game_store_api.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasedGamesController : ControllerBase
    {
        private readonly AuthHelper _auth = new();

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetPurchasedGames(int userId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            List<Game> selectedGames = PurchasedGamesService.SelectPurchasedGames(_context, userId);

            return Ok(selectedGames);
        }
    }
}
