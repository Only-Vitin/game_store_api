using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Helper;
using game_store_api.Entities;
using game_store_api.ServicesInterfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableGamesController : ControllerBase
    {
        private readonly IAuthHelper _auth;
        private readonly IUserService _userService;
        private readonly IAvailableGamesService _availableService;

        public AvailableGamesController(IAuthHelper auth, IAvailableGamesService availableService, IUserService userService)
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

            User user = _userService.GetById(userId);
            if(user == null) return NotFound();

            List<Game> availableGames = _availableService.GetById(userId);

            return Ok(availableGames);
        }
    }
}
