using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Dto;
using game_store_api.Helper;
using game_store_api.Services;
using game_store_api.Entities;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly AuthHelper _auth = new();
        private readonly GameService _gameService = new();

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetGame()
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();
            
            List<GetGameDto> gamesDto = _gameService.Get();
            return Ok(gamesDto);
        }

        [HttpGet("{gameId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetGameById(int gameId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            PostGameDto gameDto = _gameService.GetByIdDto(gameId);
            if(gameDto == null) return NotFound();

            return Ok(gameDto);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult PostGame([FromBody] PostGameDto gameDto)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            GetGameDto getGameDto = _gameService.Post(gameDto);

            return CreatedAtAction(nameof(GetGameById), new { gameId = getGameDto.GameId }, getGameDto);
        }

        [HttpPut("{gameId}")]
        [Authorize(Roles = "admin")]
        public IActionResult PutGame(int gameId, [FromBody] PostGameDto updatedGame)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            Game game = _gameService.GetById(gameId);
            if(game == null) return NotFound();

            _gameService.Put(updatedGame, game);
            
            return NoContent();
        }

        [HttpDelete("{gameId}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteGame(int gameId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();
            
            Game game = _gameService.GetById(gameId);
            if(game == null) return NotFound();

            _gameService.Delete(game);

            return NoContent();
        }
    }
}
