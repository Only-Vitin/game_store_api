using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Helper;
using game_store_api.Services;
using game_store_api.Entities;
using game_store_api.ServicesInterfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyGameController : ControllerBase
    {
        private readonly IAuthHelper _auth;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly IBuyGameService _buyGameService;

        public BuyGameController(IAuthHelper auth, IUserService userService, IGameService gameService, IBuyGameService buyGameService)
        {
            _auth = auth;
            _userService = userService;
            _gameService = gameService;
            _buyGameService = buyGameService;
        }

        [HttpPost("user/{userId}/game/{gameId}")]
        [Authorize(Roles = "user")]
        public IActionResult BuyGame(int userId, int gameId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();
            
            User user = _userService.GetById(userId);
            Game game = _gameService.GetById(gameId);
            
            if(user == null) return NotFound(new CustomMessage("Usuário não encontrado"));
            if(game == null) return NotFound(new CustomMessage("Jogo não encontrado"));

            if(!_buyGameService.VerifyOver18(game, user))
            {
                CustomMessage message = new("Necessário ter mais de 18 anos para comprar esse jogo");
                return StatusCode(StatusCodes.Status403Forbidden, message);
            }

            if(!_buyGameService.VerifyBalance(game, user))
            {
                CustomMessage message = new("Saldo insuficiente");
                return StatusCode(StatusCodes.Status403Forbidden, message);
            }

            _userService.RemoveBalance(user, game.Price);
            _buyGameService.Add(userId, gameId);

            return Ok(new CustomMessage("Compra realizada"));
        }
    }
}
