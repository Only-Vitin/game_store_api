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
    public class PurchasedGamesController : ControllerBase
    {
        private readonly Context _context;

        public PurchasedGamesController(Context context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "user")]
        public IActionResult GetPurchasedGames(int userId)
        {
            List<int> selectedGamesId = _context.PurchasedGames.Where(pg => pg.UserId == userId).Select(pg => pg.GameId).ToList();
            
            IEnumerable<Game> selectedGames =
            from id in selectedGamesId
            join game in _context.Game on id equals game.GameId
            select game;

            return Ok(selectedGames);
        }
    }
}
