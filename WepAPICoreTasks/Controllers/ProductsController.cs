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
    public class Products : ControllerBase
    {

        private readonly MyDbContext _db;
        public Products(MyDbContext db)
        {
            _db = db;

        }

        [HttpGet]
        public IActionResult Get()
        {

            var product = _db.Products.ToList();

            return Ok(product);
        }

        [Route("/api/Products/GetAll")]
        [HttpGet]
        public IActionResult GetALL()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {

            var product = _db.Products.Where(p => p.ProductId == id).FirstOrDefault();

            return Ok(product);
        }

        [HttpGet("{id:int:min(5)}")]
        public IActionResult GetById(int id)
        {
            if (id >= 0)
            {
                var product = _db.Products.Where(p => p.ProductId == id).FirstOrDefault();

                return Ok(product);
            }
            return BadRequest();
        }

        [Route("/api/Products/Id/{id:int:min(5)}")]
        [HttpGet]
        public IActionResult GetByid(int id)
        {
            if (id >= 0)
            {
                var product = _db.Products.Where(p => p.ProductId == id).FirstOrDefault();

                return Ok(product);
            }
            return BadRequest();
        }




        [HttpDelete("id1")]
        public IActionResult Delete(int id1)
        {

            var product = _db.Products.Find(id1);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        [Route("/api/Products/DeleteProduct/{id}")]
        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("name")]
        public IActionResult Name(string name)
        {
            var products = _db.Products.Where(p => p.ProductName == name);
            return Ok(products);
        }

        [HttpGet("{id:int:max(10)}/{name}")]
        public IActionResult Name(int id, string name)
        {

            var products = _db.Products.Where(p => p.ProductName == name && p.ProductId == id).FirstOrDefault();
            if (products != null)
                return Ok($"the product of the {id} is {name}");

            return NotFound();
        }



        [Route("Products/productsName/{name}/productid/{id:int:max(10)}")]
        [HttpGet]
        public IActionResult details(string name, int id)
        {

            var x = _db.Products.Where(p => p.ProductName == name && p.ProductId == id).FirstOrDefault();
            if (x != null)
                return Ok($"the id of the products id {id} name is : {name}");

            return NotFound();
        }



        [Route("Products/{id}")]
        [HttpGet]
        public IActionResult Id(int id)
        {

            var p = _db.Products.Find(id);
            if (p != null)
                return Ok($"the products of this id is  {id}");
            return NotFound();
        }








        //[HttpGet]
        //public IActionResult Get()
        //{

        //    var product = _db.Products.Include(p => p.Category).ToList();

        //    return Ok(product);
        //}





        //[HttpGet("id")]
        //public IActionResult Get(int id)
        //{

        //    var product = _db.Products.Include(p => p.Category).Where(p => p.ProductId == id).FirstOrDefault();

        //    return Ok(product);
        //}






        //[HttpGet("idd")]
        //public IActionResult Get(int idd ,decimal x)
        //{
        //    // var x  =  

        //    var product = _db.Products.Where(p => p.CategoryId == idd && (Convert.ToDecimal(p.Price) > x)).Count();

        //    return Ok(product);
        //}

    }
}