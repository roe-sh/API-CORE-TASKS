using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.netTaskNew.Models;
using Asp.netTaskNew.DTOs;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Configuration;

namespace Asp.netTaskNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db ;
        private TokenGenerator _tokenGenerator;
        public UsersController(MyDbContext context, TokenGenerator tokenGenerator)
        {
            _db = context;
            _tokenGenerator = tokenGenerator;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var category = _db.Users.ToList();
            return Ok(category);
        }

        //[HttpPost]
        //public IActionResult postProduct([FromForm] UsersDTO userDTO)
        //{


        //    var user = new User
        //    {
        //        Password = userDTO.Password,
        //        Email = userDTO.Email,

        //    };

        //    _db.Users.Add(user);
        //    _db.SaveChanges();
        //    return Ok(user);
        //}

        //[HttpPut("{id}")]
        //public IActionResult putProduct(int id, [FromForm] userRequestDTO userDTO)
        //{


        //    var x = _db.Users.FirstOrDefault(l => l.UserId == id);

        //    if (!PasswordHash.VerifyPasswordHash(userDTO.OldPassword, x.PasswordHash, x.PasswordSalt))
        //    {
        //        return Unauthorized("Old Password is Wrong");
        //    }
        //    byte[] passwordHash, passwordSalt;
        //    PasswordHash.CreatePasswordHash(userDTO.Password, out passwordHash, out passwordSalt);


        //    x.Username = userDTO.Username;
        //    x.Password = userDTO.Password;
        //    x.PasswordHash = passwordHash;
        //    x.PasswordSalt = passwordSalt;
        //    x.Email = userDTO.Email;


        //    _db.Users.Update(x);

        //    _db.SaveChanges();
        //    return Ok(x);
        //}

        [Route("{id:int}")]
        [HttpGet]
        public IActionResult getById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var categoryId = _db.Users.FirstOrDefault(l => l.UserId == id);

            return Ok(categoryId);
        }

        [Route("{name}")]
        [HttpGet]
        public IActionResult GetByName(string? name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var user = _db.Users.FirstOrDefault(l => l.Username == name);
            if (user == null)
            {

                return BadRequest();
            }

            return Ok(user);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var deleteOrders = _db.Orders.Where(l => l.UserId == id).ToList();
            foreach (var order in deleteOrders)
            {

                _db.Orders.Remove(order);
            }
            var deleteUser = _db.Users.FirstOrDefault(c => c.UserId == id);

            _db.Users.Remove(deleteUser);
            _db.SaveChanges();
            return NoContent();
        }

        //[HttpPost("Register")]
        //public IActionResult AddUser([FromForm] userRequestDTO add)
        //{
        //    byte[] hash, salt;
        //    PasswordHash.CreatePasswordHash(add.Password, out hash, out salt);
        //    var newUser = new User
        //    {
        //        Email = add.Email,
        //        Username = add.Username,
        //        Password = add.Password,
        //        PasswordSalt = salt,
        //        PasswordHash = hash
        //    };
        //    _db.Users.Add(newUser);
        //    _db.SaveChanges();
        //    return Ok(newUser);
        //}


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromForm] userRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            byte[] passwordHash, passwordSalt;
            PasswordHash.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            User user = new User
            {
                Username = "",
                Password = model.Password,
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            Cart cart = new Cart
            {
                UserId = user.UserId
            };
            await _db.Carts.AddAsync(cart);
            await _db.SaveChangesAsync();

            //For Demo Purpose we are returning the PasswordHash and PasswordSalt
            return Ok(user);
        }



        [HttpGet("GetUserInformationByName/{username}")]
        public IActionResult GetUserInformationByName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required.");
            }

            var user = _db.Users
                          .Where(u => u.Username.ToLower() == username.ToLower())
                          .Select(u => new
                          {
                              u.UserId,
                              u.Username,
                              u.Email
                          })
                          .FirstOrDefault();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }




        [HttpPost("Login")]
        public IActionResult Login([FromForm] LoginDTO user)
        {
            var dbuser = _db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (dbuser == null || !PasswordHash.VerifyPasswordHash(user.Password, dbuser.PasswordHash, dbuser.PasswordSalt))
            {
                return Unauthorized("Login Unauthorized!");
            }
            var roles = _db.UserRoles.Where(u => u.User.UserId == dbuser.UserId).Select(r => r.Role).ToList();
            var token = _tokenGenerator.GenerateToken(dbuser.Email, roles);

            return Ok(new { Token = token });

        }


    }

}