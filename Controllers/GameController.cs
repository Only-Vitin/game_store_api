using System;
using AutoMapper;
using System.Linq;
using web_api.Data;
using Microsoft.AspNetCore.Mvc;

using game_store_api.Dto;
using game_store_api.Models;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private Context _context;
        private IMapper _mapper;

        public GameController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGame()
        {
            return Ok(_context.Game);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetGameById(int id)
        {
            Game selectedGame = _context.Game.Where(i => i.GameId == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedGame == null) return NotFound();

            GameDto selectedGameDto = _mapper.Map<GameDto>(selectedGame);

            return Ok(selectedGameDto);
        }

        [HttpPost]
        public IActionResult PostGame([FromBody] GameDto gameDto)
        {
            Game game = _mapper.Map<Game>(gameDto);
            _context.Game.Add(game);
            _context.SaveChanges();

            Response.Headers.Add("Date", $"{DateTime.Now}");
            return CreatedAtAction(nameof(GetGameById), new { Id = game.GameId }, game);
        }

        [HttpPut("{id}")]
        public IActionResult PutGame(int id, [FromBody] GameDto gameDto)
        {
            Game selectedGame = _context.Game.Where(game => game.GameId == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedGame == null) return NotFound();

            _mapper.Map(gameDto, selectedGame);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            Game selectedGame = _context.Game.Where(game => game.GameId == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedGame == null) return NotFound();

            _context.Remove(selectedGame);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
