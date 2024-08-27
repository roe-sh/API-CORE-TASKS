using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4.DTO_S;
using Task4.Models;

namespace Task4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("AddUser")]
        public IActionResult addUser([FromForm] userDTO user)
        {


            var newUser = new User()
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("EditUser{id}")]
        public IActionResult editCategort(int id, [FromForm] userDTO user)
        {


            var userUpdate = _context.Users.Find(id);

            userUpdate.Username = user.Username;
            userUpdate.Password = user.Password;
            userUpdate.Email = user.Email;

            _context.Users.Update(userUpdate);
            _context.SaveChanges();
            return Ok();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
