using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailableGamesController : ControllerBase
    {
        private readonly Context _context;

        public AvailableGamesController(Context context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "user")]
        public IActionResult GetAvailableGames(int userId)
        {
            List<int> selectedGamesId = _context.PurchasedGames.Where(pg => pg.UserId == userId).Select(pg => pg.GameId).ToList();
            
            List<Game> selectedGames =
            (from game in _context.Game
            where !selectedGamesId.Contains(game.GameId)
            select game).ToList();

            return Ok(selectedGames);
        }
    }
}