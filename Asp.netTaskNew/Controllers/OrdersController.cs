using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.netTaskNew.Models;

namespace Asp.netTaskNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _db;
        public OrdersController(MyDbContext db)
        {

            _db = db;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var order = _db.Orders.ToList();
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
               
            return Ok(order);
        }

        [Route("/api/Orders/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var order = _db.Orders.ToList();
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }


        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var x = _db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            if (x != null)
            {
                return Ok(x);
            }
            return NotFound();
        }

        [Route("Orders/{id}")]
        [HttpGet]
        public IActionResult GetID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var x = _db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            if (x != null)
            {
                return Ok(x);
            }
            return NotFound();
        }



        [HttpDelete("id1")]
        public IActionResult Delete(int id1)
        {
            var order = _db.Orders.Find(id1);
            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }


        [Route("/api/Orders/DeleteOrder/{id}")]
        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            var order = _db.Orders.Find(id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
    }
}