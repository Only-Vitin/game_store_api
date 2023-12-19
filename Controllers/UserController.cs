using System;
using AutoMapper;
using System.Linq;
using web_api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net.BCrypt;

using game_store_api.Models;
using game_store_api.Dto;
using game_store_api.Utils;
using game_store_api.Service;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private Context _context;
        private IMapper _mapper;

        public UserController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private readonly UserService userService = new();

        [HttpGet]
        public IActionResult GetUser()
        {
            List<GetUserDto> usersDto = userService.GetUserService(_context, _mapper);
            return Ok(usersDto);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            User selectedUser = _context.User.Where(i => i.UserId == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedUser == null) return NotFound();

            GetUserByIdDto selectedUserDto = _mapper.Map<GetUserByIdDto>(selectedUser);

            return Ok(selectedUserDto);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] PostUserDto userDto)
        {
            if(!userService.VerifyEmailOnDb(userDto, _context))
            {
                CustomMessage customMessage = new("O email já existe no banco de dados");
                return Conflict(customMessage);
            }
            
            User user = _mapper.Map<User>(userDto);
            user.Password = BCryptNet.EnhancedHashPassword(user.Password, 13);

            _context.User.Add(user);
            _context.SaveChanges();

            GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
            Response.Headers.Add("Date", $"{DateTime.Now}");
            return CreatedAtAction(nameof(GetUserById), new { Id = userGetDto.UserId }, userGetDto);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] PostUserDto userDto)
        {
            User selectedUser = _context.User.Where(user => user.UserId == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedUser == null) return NotFound();

            userDto.Password = BCryptNet.EnhancedHashPassword(userDto.Password, 13);
            _mapper.Map(userDto, selectedUser);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User selectedUser = _context.User.Where(user => user.UserId == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedUser == null) return NotFound();

            _context.Remove(selectedUser);
            _context.SaveChanges();

            return NoContent();
        }
    }
}