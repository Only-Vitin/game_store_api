using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;
        private readonly IResponseHelper _respHelper;
        private readonly IGameService _gameService;
        private readonly IGameStorage _gameStorage;

        public GameController(IAuthHelper authHelper, IResponseHelper respHelper,
            IMapper mapper, IGameService gameService, IGameStorage gameStorage)
        {
            _gameService = gameService;
            _gameStorage = gameStorage;
            _authHelper = authHelper;
            _respHelper = respHelper;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetGame()
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();
            
            List<GetGameDto> gamesDto = _gameService.GetGameService();
            return Ok(gamesDto);
        }
        
        [HttpGet("{gameId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetGameById(int gameId)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();

            Game selectedGame = _gameStorage.SelectById(gameId);
            if(selectedGame == null) return NotFound();

            PostGameDto selectedGameDto = _mapper.Map<PostGameDto>(selectedGame);

            return Ok(selectedGameDto);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult PostGame([FromBody] PostGameDto gameDto)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();

            Game game = _mapper.Map<Game>(gameDto);
            _gameStorage.AddGame(game);

            GetGameDto getGameDto = _mapper.Map<GetGameDto>(game);
            return CreatedAtAction(nameof(GetGameById), new { gameId = getGameDto.GameId }, getGameDto);
        }

        [HttpPut("{gameId}")]
        [Authorize(Roles = "admin")]
        public IActionResult PutGame(int gameId, [FromBody] PostGameDto gameDto)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();

            Game game = _gameStorage.SelectById(gameId);
            if(game == null) return NotFound();

            _gameStorage.PutGame(gameDto, game);
            
            return NoContent();
        }

        [HttpDelete("{gameId}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteGame(int gameId)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();
            
            Game game = _gameStorage.SelectById(gameId);
            if(game == null) return NotFound();

            _gameStorage.RemoveGame(game);

            return NoContent();
        }
    }
}
