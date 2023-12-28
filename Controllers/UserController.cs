using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Utils;
using game_store_api.Service;
using game_store_api.Entities;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetUser()
        {
            Response.Headers.Add("Date", $"{DateTime.Now}");

            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();

            List<GetUserDto> usersDto = UserService.GetUserService(_context, _mapper);
            return Ok(usersDto);
        }
        
        [HttpGet("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetUserById(int userId)
        {
            Response.Headers.Add("Date", $"{DateTime.Now}");

            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();

            User selectedUser = _context.User.Where(u => u.UserId == userId).SingleOrDefault();
            if(selectedUser == null) return NotFound();

            GetUserByIdDto selectedUserDto = _mapper.Map<GetUserByIdDto>(selectedUser);
            return Ok(selectedUserDto);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] PostUserDto userDto)
        {
            Response.Headers.Add("Date", $"{DateTime.Now}");

            if(UserService.VerifyEmailOnDb(userDto, _context))
            {
                CustomMessage customMessage = new("O email já existe no banco de dados");
                return Conflict(customMessage);
            }
            
            GetUserDto userGetDto = UserService.AddUserOnDb(userDto, _context, _mapper);
            return CreatedAtAction(nameof(GetUserById), new { userGetDto.UserId }, userGetDto);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult PutUser(int userId, [FromBody] PostUserDto userDto)
        {
            Response.Headers.Add("Date", $"{DateTime.Now}");
            
            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();
            
            User selectedUser = _context.User.Where(u => u.UserId == userId).SingleOrDefault();
            if(selectedUser == null) return NotFound();

            UserService.PutUserService(userDto, selectedUser, _context, _mapper);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult DeleteUser(int userId)
        {
            Response.Headers.Add("Date", $"{DateTime.Now}");

            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();
            
            User selectedUser = _context.User.Where(u => u.UserId == userId).SingleOrDefault();
            if(selectedUser == null) return NotFound();

            _context.Remove(selectedUser);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
