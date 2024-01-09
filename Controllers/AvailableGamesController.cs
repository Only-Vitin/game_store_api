using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableGamesController : ControllerBase
    {
        private readonly IAuthHelper _authHelper;
        private readonly IUserStorage _userStorage;
        private readonly IResponseHelper _respHelper;
        private readonly IAvailableGamesService _availableService;

        public AvailableGamesController(IAuthHelper authHelper, IResponseHelper respHelper,
            IAvailableGamesService availableService, IUserStorage userStorage)
        {
            _authHelper = authHelper;
            _respHelper = respHelper;
            _availableService = availableService;
            _userStorage = userStorage;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetAvailableGames(int userId)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();

            User user = _userStorage.SelectById(userId);
            if(user == null) return NotFound();

            List<Game> games = _availableService.SelectAvailableGames(userId);

            return Ok(games);
        }
    }
}
