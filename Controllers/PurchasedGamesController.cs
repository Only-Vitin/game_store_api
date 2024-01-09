using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasedGamesController : ControllerBase
    {
        private readonly IResponseHelper _respHelper;
        private readonly IUserStorage _userStorage;
        private readonly IPurchasedGamesService _purchasedService;
        private readonly IAuthHelper _authHelper;

        public PurchasedGamesController(IResponseHelper respHelper, IAuthHelper authHelper,
            IPurchasedGamesService purchasedService, IUserStorage userStorage)
        {
            _respHelper = respHelper;
            _authHelper = authHelper;
            _purchasedService = purchasedService;
            _userStorage = userStorage;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetPurchasedGames(int userId)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();

            User user = _userStorage.SelectById(userId);
            if(user == null) return NotFound();

            List<Game> selectedGames = _purchasedService.SelectPurchasedGames(userId);

            return Ok(selectedGames);
        }
    }
}
