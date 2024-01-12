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
    public class PurchasedGamesController : ControllerBase
    {
        private readonly IAuthHelper _auth;
        private readonly IUserService _userService;
        private readonly IPurchasedGamesService _purchasedService;

        public PurchasedGamesController(IAuthHelper auth, IPurchasedGamesService purchasedService, IUserService userService)
        {
            _auth = auth;
            _userService = userService;
            _purchasedService = purchasedService;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetPurchasedGames(int userId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            User user = _userService.GetById(userId);
            if(user == null) return NotFound();

            List<Game> purchasedGames = _purchasedService.GetById(userId);

            return Ok(purchasedGames);
        }
    }
}
