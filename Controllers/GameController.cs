using AutoMapper;
using System.Linq;
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
        private readonly GameService _gameService;

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetGame()
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();
            
            List<GetGameDto> gamesDto = _gameService.GetGameService();
            return Ok(gamesDto);
        }
        
        [HttpGet("{gameId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetGameById(int gameId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            Game selectedGame = _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();
            if(selectedGame == null) return NotFound();

            PostGameDto selectedGameDto = _mapper.Map<PostGameDto>(selectedGame);

            return Ok(selectedGameDto);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult PostGame([FromBody] PostGameDto gameDto)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            Game game = _mapper.Map<Game>(gameDto);
            _context.Game.Add(game);
            _context.SaveChanges();

            GetGameDto getGameDto = _mapper.Map<GetGameDto>(game);
            return CreatedAtAction(nameof(GetGameById), new { gameId = getGameDto.GameId }, getGameDto);
        }

        [HttpPut("{gameId}")]
        [Authorize(Roles = "admin")]
        public IActionResult PutGame(int gameId, [FromBody] PostGameDto gameDto)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            Game selectedGame = _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();
            if(selectedGame == null) return NotFound();

            _mapper.Map(gameDto, selectedGame);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{gameId}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteGame(int gameId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();
            
            Game selectedGame = _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();
            if(selectedGame == null) return NotFound();

            _context.Remove(selectedGame);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
