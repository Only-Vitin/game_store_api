using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Data;
using game_store_api.Utils;
using game_store_api.Entities;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddBalance : ControllerBase
    {
        private readonly Context _context;

        public AddBalance(Context context)
        {
            _context = context;
        }

        [HttpPost("user/{userId}/value/{value}")]
        [Authorize(Roles = "user")]
        public IActionResult AddBalanceByUserId(int userId, double value)
        {
            User selectedUser = _context.User.Where(u => u.UserId == userId).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedUser == null) return NotFound();

            selectedUser.Balance += value;
            _context.SaveChanges();
            
            CustomMessage customMessage = new("Valor adicionado com sucesso");
            return Ok(customMessage);
        }
    }
}
