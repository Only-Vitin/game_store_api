using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
    public class AvailableGamesController : ControllerBase
    {
        private readonly Context _context;

        public AvailableGamesController(Context context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetAvailableGames(int userId)
        {
            Response.Headers.Add("Date", $"{DateTime.Now}");

            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();

            List<Game> selectedGames = AvailableGamesService.SelectAvailableGames(_context, userId);

            return Ok(selectedGames);
        }
    }
}
