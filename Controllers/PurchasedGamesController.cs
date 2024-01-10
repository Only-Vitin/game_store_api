using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Data;
using game_store_api.Utils;
using game_store_api.Service;
using game_store_api.Entities;

namespace game_store_api.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasedGamesController : ControllerBase
    {
        private readonly Context _context;

        public PurchasedGamesController(Context context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetPurchasedGames(int userId)
        {
            Response.Headers.Add("Date", $"{DateTime.Now}");

            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();

            List<Game> selectedGames = PurchasedGamesService.SelectPurchasedGames(_context, userId);

            return Ok(selectedGames);
        }
    }
}
