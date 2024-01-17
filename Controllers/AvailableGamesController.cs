using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Helper;
using game_store_api.Entities;
using game_store_api.Services;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableGamesController : ControllerBase
    {
        private readonly AuthHelper _auth;
        private readonly UserService _userService;
        private readonly AvailableGamesService _availableService;

        public AvailableGamesController(AuthHelper auth, AvailableGamesService availableService, UserService userService)
        {
            _auth = auth;
            _userService = userService;
            _availableService = availableService;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetAvailableGames(int userId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            if(!_userService.VerifyUserId(userId)) return NotFound();

            List<Game> availableGames = _availableService.GetById(userId);

            return Ok(availableGames);
        }
    }
}
