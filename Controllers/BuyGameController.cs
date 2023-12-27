using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Utils;
using game_store_api.Service;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyGameController : ControllerBase
    {
        private readonly Context _context;

        public BuyGameController(Context context)
        {
            _context = context;
        }

        [HttpPost("user/{userId}/game/{gameId}")]
        [Authorize(Roles = "user")]
        public IActionResult BuyGame(int userId, int gameId)
        {
            User userToBuy = _context.User.Where(u => u.UserId == userId).SingleOrDefault();
            Game gameToBuy = _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();

            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(userToBuy == null)
            {
                CustomMessage message = new("Usuário não encontrado");
                return NotFound(message);
            }
            if(gameToBuy == null)
            {
                CustomMessage message = new("Jogo não encontrado");
                return NotFound();
            }

            if(!BuyGameService.VerifyOver18(gameToBuy.Over18, userToBuy.Age))
            {
                return Forbid("Necessário ter mais de 18 anos para comprar esse jogo");
            }

            if(userToBuy.Balance < gameToBuy.Price) return Forbid("Saldo insuficiente");
            

            userToBuy.Balance -= gameToBuy.Price;
            BuyGameService.AddPurchaseOnDb(userId, gameId, _context);
           
            CustomMessage customMessage = new("Compra realizada");
            return Ok(customMessage);
        }
    }
}
