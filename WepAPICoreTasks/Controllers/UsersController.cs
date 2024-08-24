using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly MyDbContext _db;
        public UsersController(MyDbContext db)
        {

            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = _db.Users.ToList();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Route("/api/Users/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var user = _db.Users.ToList();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var x = _db.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (x != null)
            {
                return Ok(x);
            }
            return NotFound();

        }

        [Route("Users/{id}")]
        [HttpGet]
        public IActionResult GetID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var x = _db.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (x != null)
            {
                return Ok(x);
            }
            return NotFound();

        }



        [HttpGet("name")]
        public IActionResult Getname(string name)
        {
            var user = _db.Users.Where(p => p.Username == name);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();

        }

        [Route("username/{name}")]
        [HttpGet]
        public IActionResult GetName(string name)
        {
            var user = _db.Users.Where(p => p.Username == name).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }


            return Ok(user);
        }


        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest();

            }

            var x = _db.Orders.Where(p => p.UserId == id).ToList();

            _db.Orders.RemoveRange(x);
            _db.SaveChanges();

            var y = _db.Users.Where(_ => _.UserId == id).FirstOrDefault();
            if (y != null)
            {

                _db.Users.Remove(y);
                _db.SaveChanges();
                return NoContent();


            }

            return NotFound();



        }



        [Route("/api/Users/Deleteuser/{id}")]

        [HttpDelete]
        public IActionResult DeleteBYId(int id)
        {

            if (id <= 0)
            {
                return BadRequest();

            }

            var x = _db.Orders.Where(p => p.UserId == id).ToList();

            _db.Orders.RemoveRange(x);
            _db.SaveChanges();

            var y = _db.Users.Where(_ => _.UserId == id).FirstOrDefault();
            if (y != null)
            {

                _db.Users.Remove(y);
                _db.SaveChanges();
                return NoContent();


            }

            return NotFound();



        }
    }
}