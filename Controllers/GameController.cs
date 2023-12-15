using System;
using System.Linq;
using AutoMapper;
using game_store_api.Models;
using Microsoft.AspNetCore.Mvc;
using web_api.Data;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private GameContext _context;
        private IMapper _mapper;

        public GameController(GameContext context, IMapper mapper)
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
            Game selectedGame = _context.Game.FirstOrDefault(i => i.Id == id);

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
            return CreatedAtAction(nameof(GetGameById), new { Id = game.Id }, game);
        }
    }
}