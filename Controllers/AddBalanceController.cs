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
    [Route("api/[controller]")]
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
            Response.Headers.Add("Date", $"{DateTime.Now}");
            
            string authorization = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            if(!VerifyToken.VerifyTokenOnDb(authorization, _context)) return Unauthorized();

            User selectedUser = _context.User.Where(u => u.UserId == userId).SingleOrDefault();
            if(selectedUser == null) return NotFound();

            selectedUser.Balance += value;
            _context.SaveChanges();
            
            CustomMessage customMessage = new("Valor adicionado com sucesso");
            return Ok(customMessage);
        }
    }
}
