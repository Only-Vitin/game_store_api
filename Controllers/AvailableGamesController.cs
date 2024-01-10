using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Entities;
using game_store_api.Data;
using game_store_api.Helper;
using game_store_api.Services;

namespace game_store_api.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableGamesController : ControllerBase
    {
        private readonly AuthHelper _auth = new();
        
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetAvailableGames(int userId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            List<Game> selectedGames = AvailableGamesService.SelectAvailableGames(_context, userId);

            return Ok(selectedGames);
        }
    }
}
