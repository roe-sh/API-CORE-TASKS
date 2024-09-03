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

namespace Asp.netTaskNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UsersController(MyDbContext context)
        {
            _db = context;
        }

        [Route("api/Users/GetAllUsers")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var Users = _db.Users.ToList();
            return Ok(Users);
        }


        [Route("/api/Users/GetUserById")]
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            var User = _db.Users.Where(p => p.UserId == id).FirstOrDefault();
            if (User == null)
            {
                return BadRequest();
            }
            return Ok(User);
        }


        [Route("/api/Users/GetUserByName/{name}")]
        [HttpGet]
        public IActionResult GetUserByName(string name)
        {
            var user = _db.Users.Where(p => p.Username == name).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [Route("/api/Users/DeleteUser/{id}")]
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var orders = _db.Orders.Where(p => p.UserId == id).ToList();
            _db.Orders.RemoveRange(orders);
            _db.SaveChanges();

            var user = _db.Users.FirstOrDefault(p => p.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            _db.Users.Remove(user);
            _db.SaveChanges();
            return Ok("the user has been deleted");
        }



        [HttpPost("Regester")]
        public IActionResult AddUser([FromForm] userRequestDTO add)
        {
            byte[] hash, salt;
            PasswordHash.CreatePasswordHash(add.Password, out hash, out salt);
            var newuser = new User()
            {
                Email = add.Email,
                Username = add.Username,
                Password = add.Password,
                PasswordSalt = salt,
                PasswordHash = hash

            };
            _db.Users.Add(newuser);
            _db.SaveChanges();
            return Ok(newuser);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] LoginDTO user)
        {
            var userR = _db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userR == null || !PasswordHash.VerifyPasswordHash(user.Password, userR.PasswordHash, userR.PasswordSalt))
            {
                return Unauthorized("bad life");
            }
            return Ok("good life!!");

        }


        [HttpPut("{id}")]
        public IActionResult editUser(int id, [FromForm] userRequestDTO edit)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return BadRequest();
            }

            user.Email = edit.Email;
            user.Username = edit.Username;
            user.Password = edit.Password;

            _db.Users.Update(user);
            _db.SaveChanges();
            return Ok(user);
        }


       
       

       

        

        [HttpGet("get/{username}")]
        public IActionResult getuserbyname(string username)
        {

            var user = _db.Users.FirstOrDefault(u => u.Username == username);
            return Ok(user);

        }


    }
}