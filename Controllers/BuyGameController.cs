using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Utils;
using game_store_api.Service;
using game_store_api.Entities;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyGameController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public BuyGameController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("user/{userId}/game/{gameId}")]
        public IActionResult BuyGame()
        {
            return Ok("OIII");
        }
    }
}
