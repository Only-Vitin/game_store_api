using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Helper;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyGameController : ControllerBase
    {
        private readonly IAuthHelper _authHelper;
        private readonly IResponseHelper _respHelper;
        private readonly IUserStorage _userStorage;
        private readonly IGameStorage _gameStorage;
        private readonly IBuyGameService _buyGameService;

        public BuyGameController(IAuthHelper authHelper, IResponseHelper respHelper,
            IUserStorage userStorage, IGameStorage gameStorage, IBuyGameService buyGameService)
        {
            _authHelper = authHelper;
            _respHelper = respHelper;
            _userStorage = userStorage;
            _gameStorage = gameStorage;
            _buyGameService = buyGameService;
        }

        [HttpPost("user/{userId}/game/{gameId}")]
        [Authorize(Roles = "user")]
        public IActionResult BuyGame(int userId, int gameId)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();
            
            User user = _userStorage.SelectById(userId);
            Game game = _gameStorage.SelectById(gameId);

            string response = _buyGameService.ValidProvidedId(user, game);
            if(response != "Ok") return NotFound(new CustomMessage(response));

            response = _buyGameService.ValidUserForPurchase(user, game);
            if(response != "Ok") return StatusCode(StatusCodes.Status403Forbidden, new CustomMessage(response));

            _userStorage.DiscountGamePrice(user, game);
            _buyGameService.AddPurchaseOnDb(userId, gameId);

            return Ok(new CustomMessage("Compra efetuada"));
        }
    }
}
