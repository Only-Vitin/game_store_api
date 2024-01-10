using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Helper;
using game_store_api.Services;

namespace game_store_api.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class BuyGameController : ControllerBase
    {
        private readonly AuthHelper _auth = new();

        [HttpPost("user/{userId}/game/{gameId}")]
        [Authorize(Roles = "user")]
        public IActionResult BuyGame(int userId, int gameId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();
            
            User userToBuy = _context.User.Where(u => u.UserId == userId).SingleOrDefault();
            Game gameToBuy = _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();

            if(userToBuy == null)
            {
                CustomMessage message = new("Usuário não encontrado");
                return NotFound(message);
            }
            if(gameToBuy == null)
            {
                CustomMessage message = new("Jogo não encontrado");
                return NotFound(message);
            }

            if(!BuyGameService.VerifyOver18(gameToBuy.Over18, userToBuy.Age))
            {   
                CustomMessage message = new("Necessário ter mais de 18 anos para comprar esse jogo");
                return StatusCode(StatusCodes.Status403Forbidden, message);
            }

            if(userToBuy.Balance < gameToBuy.Price) 
            {
                CustomMessage message = new("Saldo insuficiente");
                return StatusCode(StatusCodes.Status403Forbidden, message);
            }

            userToBuy.Balance -= gameToBuy.Price;
            BuyGameService.AddPurchaseOnDb(userId, gameId, _context);
           
            CustomMessage customMessage = new("Compra realizada");
            return Ok(customMessage);
        }
    }
}
