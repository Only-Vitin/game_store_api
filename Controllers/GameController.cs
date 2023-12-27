using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Service;
using game_store_api.Utils;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public GameController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetGame()
        {
            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();
            
            List<GetGameDto> gamesDto = GameService.GetGameService(_context, _mapper);
            return Ok(gamesDto);
        }
        
        [HttpGet("{gameId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetGameById(int gameId)
        {
            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();

            Game selectedGame = _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedGame == null) return NotFound();

            PostGameDto selectedGameDto = _mapper.Map<PostGameDto>(selectedGame);

            return Ok(selectedGameDto);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult PostGame([FromBody] PostGameDto gameDto)
        {
            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();

            Game game = _mapper.Map<Game>(gameDto);
            _context.Game.Add(game);
            _context.SaveChanges();

            Response.Headers.Add("Date", $"{DateTime.Now}");

            GetGameDto getGameDto = _mapper.Map<GetGameDto>(game);
            return CreatedAtAction(nameof(GetGameById), new { gameId = getGameDto.GameId }, getGameDto);
        }

        [HttpPut("{gameId}")]
        [Authorize(Roles = "admin")]
        public IActionResult PutGame(int gameId, [FromBody] PostGameDto gameDto)
        {
            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();

            Game selectedGame = _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedGame == null) return NotFound();

            _mapper.Map(gameDto, selectedGame);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{gameId}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteGame(int gameId)
        {
            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();
            
            Game selectedGame = _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedGame == null) return NotFound();

            _context.Remove(selectedGame);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
